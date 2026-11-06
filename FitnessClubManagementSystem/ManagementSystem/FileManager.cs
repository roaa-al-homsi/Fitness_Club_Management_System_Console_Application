using FitnessClubManagementSystem.Core;
using FitnessClubManagementSystem.Core.Activities;
using FitnessClubManagementSystem.Lib;
using System;
using System.Globalization;
using System.IO;

namespace FitnessClubManagementSystem.ManagementSystem
{
    // Handles reading and writing gym data using text files.
    public static class FileManager
    {
        const char Separator = ';';

        // Dates are written in ISO format so the files mean the same thing on every machine.
        // Older records written as dd/MM/yyyy are still read (see clsInputValidate.TryParseDate).
        const string FileDateFormat = "yyyy-MM-dd";

        // File paths used to store the system data.
        // They are resolved next to the executable (not the current directory) and built with Path.Combine,
        // so they work on Windows, Linux and macOS whichever folder the app is started from.
        static readonly string DataFolder = Path.Combine(AppContext.BaseDirectory, "Data");
        static readonly string MembersFile = Path.Combine(DataFolder, "MEMBERS.txt");
        static readonly string ActivitiesFile = Path.Combine(DataFolder, "ACTIVITIES.txt");
        static readonly string TrainersFile = Path.Combine(DataFolder, "TRAINERS.txt");
        static readonly string BookingsFile = Path.Combine(DataFolder, "BOOKINGS.txt");

        /* Shared helpers */

        // Loads every record of a data file into an array of the given capacity and returns how many were loaded.
        // A malformed line is reported and skipped, so one bad record no longer stops the rest of the file from loading.
        static T[] _LoadRecords<T>(string filePath, int capacity, Func<string, T> convertLine, out int count)
        {
            T[] records = new T[capacity];
            count = 0;

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Data file not found: {filePath}. Starting with no records.");
                return records;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
            {
                Console.WriteLine(e.Message);
                return records;
            }

            string fileName = Path.GetFileName(filePath);

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                if (count >= capacity)
                {
                    Console.WriteLine($"{fileName}: only the first {capacity} records were loaded.");
                    break;
                }

                try
                {
                    records[count] = convertLine(lines[i]);
                    count++;
                }
                catch (Exception e) when (e is FormatException || e is OverflowException || e is ArgumentException)
                {
                    Console.WriteLine($"{fileName} line {i + 1} skipped ({e.Message}): {lines[i]}");
                }
            }

            return records;
        }

        // Appends one record to a data file, creating the Data folder and the file when they don't exist yet.
        static bool _AppendRecord(string filePath, string record)
        {
            try
            {
                Directory.CreateDirectory(DataFolder);

                // If the file was edited by hand and its last line has no line break,
                // start the new record on its own line instead of gluing it onto the previous one.
                string prefix = _EndsWithLineBreak(filePath) ? "" : Environment.NewLine;
                File.AppendAllText(filePath, prefix + record + Environment.NewLine);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        static bool _EndsWithLineBreak(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return true;
            }

            using (FileStream stream = File.OpenRead(filePath))
            {
                if (stream.Length == 0)
                {
                    return true;
                }

                stream.Seek(-1, SeekOrigin.End);
                return stream.ReadByte() == '\n';
            }
        }

        // Splits a record into trimmed fields and checks it has at least the expected number of them.
        static string[] _SplitRecord(string stLine, int minFields)
        {
            string[] fields = stLine.Split(Separator);
            _RequireFields(fields, minFields);

            for (int i = 0; i < fields.Length; i++)
            {
                fields[i] = fields[i].Trim();
            }
            return fields;
        }

        static void _RequireFields(string[] fields, int minFields)
        {
            if (fields.Length < minFields)
            {
                throw new FormatException($"expected {minFields} fields but found {fields.Length}");
            }
        }

        static int _ParseInt(string text)
        {
            return int.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
        }

        static double _ParseDouble(string text)
        {
            return double.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        static DateTime _ParseDate(string text)
        {
            if (!clsInputValidate.TryParseDate(text, out DateTime date))
            {
                throw new FormatException($"'{text}' is not a valid date");
            }
            return date;
        }

        static string _FormatDate(DateTime date)
        {
            return date.ToString(FileDateFormat, CultureInfo.InvariantCulture);
        }

        static string _FormatDouble(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        // Removes characters that would break the one-record-per-line, semicolon-separated format.
        static string _CleanText(string text)
        {
            if (text == null)
            {
                return "";
            }
            return text.Replace(Separator, ' ').Replace('\r', ' ').Replace('\n', ' ').Trim();
        }

        /* Member */

        // Converts a text record into the appropriate Member object.
        static Member _ConvertStLineToMemberObject(string stLine)
        {
            string[] fields = _SplitRecord(stLine, 6);

            int id = _ParseInt(fields[0]);
            DateTime joinDate = _ParseDate(fields[3]);

            switch (fields[4].ToUpperInvariant())
            {
                case "STANDARD":
                    return new StandardMember(id, fields[1], fields[2], joinDate, _ParseInt(fields[5]));

                case "PREMIUM":
                    _RequireFields(fields, 7);
                    return new PremiumMember(id, fields[1], fields[2], joinDate, bool.Parse(fields[5]), _ParseInt(fields[6]));

                default:
                    throw new FormatException($"unknown member type '{fields[4]}'");
            }
        }

        // Converts a Member object into a semicolon-separated text record.
        public static string ConvertMemberObjectToStLine(Member member)
        {
            string textObjectMember = member.Id.ToString(CultureInfo.InvariantCulture) + Separator;
            textObjectMember += _CleanText(member.Name) + Separator;
            textObjectMember += _CleanText(member.Email) + Separator;
            textObjectMember += _FormatDate(member.JoinDate) + Separator;

            if (member is StandardMember standardMember)
            {
                textObjectMember += "STANDARD" + Separator;
                textObjectMember += standardMember.FreezeDaysUsed.ToString(CultureInfo.InvariantCulture) + Separator + "0";
            }
            else if (member is PremiumMember premiumMember)
            {
                string textHasPersonalTrainer = premiumMember.HasPersonalTrainer ? "True" : "False";
                textObjectMember += "PREMIUM" + Separator;
                textObjectMember += textHasPersonalTrainer + Separator;
                textObjectMember += premiumMember.GuestPasses.ToString(CultureInfo.InvariantCulture);
            }
            return textObjectMember;
        }

        //Load Data From File To Array Members
        public static Member[] LoadMembers()
        {
            return _LoadRecords(MembersFile, FitnessClubSystem.MaxMembers, _ConvertStLineToMemberObject, out FitnessClubSystem.MemberCount);
        }

        // Appends a new member record to the members file.
        public static bool SaveMember(string memberRecord)//Add member record to file.
        {
            return _AppendRecord(MembersFile, memberRecord);
        }

        /* Activity */

        // Converts a text record into the appropriate Activity object.
        static Activity _ConvertStLineToActivityObject(string stLine)
        {
            string[] fields = _SplitRecord(stLine, 7);

            int id = _ParseInt(fields[0]);
            int durationMinutes = _ParseInt(fields[2]);
            int capacity = _ParseInt(fields[3]);

            switch (fields[4].ToUpperInvariant())
            {
                case "GROUP":
                    return new GroupClass(id, fields[1], durationMinutes, capacity, fields[5], fields[6]);

                case "PT":
                    return new PersonalTrainingSession(id, fields[1], durationMinutes, capacity, _ParseInt(fields[5]), _ParseDouble(fields[6]));

                default:
                    throw new FormatException($"unknown activity type '{fields[4]}'");
            }
        }

        //Load Data From File To Array Activities
        public static Activity[] LoadActivities()
        {
            return _LoadRecords(ActivitiesFile, FitnessClubSystem.MaxActivity, _ConvertStLineToActivityObject, out FitnessClubSystem.ActivityCount);
        }

        // Converts an Activity object into a text record according to its type.
        public static string ConvertActivityObjectToStLine(Activity activity)
        {
            string activityLine = activity.Id.ToString(CultureInfo.InvariantCulture) + Separator;
            activityLine += _CleanText(activity.Name) + Separator;
            activityLine += activity.DurationMinutes.ToString(CultureInfo.InvariantCulture) + Separator;
            activityLine += activity.Capacity.ToString(CultureInfo.InvariantCulture) + Separator;

            if (activity is GroupClass groupClass)
            {
                activityLine += "GROUP" + Separator;
                activityLine += _CleanText(groupClass.InstructorName) + Separator;
                activityLine += groupClass.DifficultyLevel == "beg" ? "Beginner" : "Advanced";

                return activityLine;
            }
            else if (activity is PersonalTrainingSession personalTrainingSession)
            {
                activityLine += "PT" + Separator;
                activityLine += personalTrainingSession.TrainerId.ToString(CultureInfo.InvariantCulture) + Separator;
                activityLine += _FormatDouble(personalTrainingSession.HourlyRate);

                return activityLine;
            }
            return " ";
        }

        // Appends a new activity record to the activities file.
        public static bool SaveActivity(string activityRecord)
        {
            return _AppendRecord(ActivitiesFile, activityRecord);
        }

        /* Trainers */

        // Converts a text record into a Trainer object.
        static Trainer _ConvertStLineToTrainerObject(string stLine)
        {
            string[] fields = _SplitRecord(stLine, 4);
            return new Trainer(_ParseInt(fields[0]), fields[1], fields[2], _ParseDouble(fields[3]));
        }

        //Load Data From File To Array Trainers
        public static Trainer[] LoadTrainers()
        {
            return _LoadRecords(TrainersFile, FitnessClubSystem.MaxTrainers, _ConvertStLineToTrainerObject, out FitnessClubSystem.TrainerCount);
        }

        // Converts a Trainer object into a semicolon-separated text record.
        public static string ConvertTrainerObjectToStLine(Trainer trainer)
        {
            string trainerLine = trainer.Id.ToString(CultureInfo.InvariantCulture) + Separator;
            trainerLine += _CleanText(trainer.Name) + Separator;
            trainerLine += _CleanText(trainer.Specialty) + Separator;
            trainerLine += _FormatDouble(trainer.Salary);
            return trainerLine;
        }

        // Appends a new trainer record to the trainers file.
        public static bool SaveTrainer(string TrainerRecord)//Add new trainer to file.
        {
            return _AppendRecord(TrainersFile, TrainerRecord);
        }

        /*BOOKING*/

        // Converts a text record into a Booking object.
        static Booking _ConvertStLineToBookingObject(string stLine)
        {
            string[] fields = _SplitRecord(stLine, 5);
            return new Booking(_ParseInt(fields[0]), _ParseInt(fields[1]), _ParseInt(fields[2]), _ParseDate(fields[3]), fields[4]);
        }

        // Converts a Booking object into a semicolon-separated text record.
        public static string ConvertBookingObjectToStLine(Booking booking)
        {
            string StLBooking = booking.Id.ToString(CultureInfo.InvariantCulture) + Separator;
            StLBooking += booking.MemberId.ToString(CultureInfo.InvariantCulture) + Separator;
            StLBooking += booking.ActivityId.ToString(CultureInfo.InvariantCulture) + Separator;
            StLBooking += _FormatDate(booking.Date) + Separator;
            StLBooking += booking.Status;

            return StLBooking;
        }

        //Load Data From File To Array Bookings
        public static Booking[] LoadBookings()
        {
            return _LoadRecords(BookingsFile, FitnessClubSystem.MaxBookings, _ConvertStLineToBookingObject, out FitnessClubSystem.BookingCount);
        }

        // Appends a new booking record to the bookings file.
        public static bool SaveBooking(string BookingRecord)//Add new Booking to file.
        {
            return _AppendRecord(BookingsFile, BookingRecord);
        }

    }
}
