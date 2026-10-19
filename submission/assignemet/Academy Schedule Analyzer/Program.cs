

using Academy_Schedule_Analyzer;
using BenchmarkDotNet.Running;
using System.Globalization;
using System.Text;

string[] sessionNames =
{
    "C# Basics",
    "Arrays",
    "Functions",
    "Date and Time",
    "Exception Handling"
};

DateTime[] sessionDates =
{
    new DateTime(2026, 9, 10, 18, 0, 0),
    new DateTime(2026, 9, 13, 18, 0, 0),
    new DateTime(2026, 9, 17, 18, 0, 0),
    new DateTime(2026, 11, 20, 18, 0, 0),
    new DateTime(2026, 12, 24, 18, 0, 0)
};

int[] sessionDurations =
{
    180,
    240,
    180,
    240,
    180
};








    bool isRunning = true;

    while (isRunning)
    {
        Console.WriteLine("===================================");
        Console.WriteLine("Academy Schedule Analyzer");
        Console.WriteLine("===================================");
        Console.WriteLine("1. Display all sessions");
        Console.WriteLine("2. Search for a session");
        Console.WriteLine("3. Sort session names");
        Console.WriteLine("4. Reverse session names");
        Console.WriteLine("5. Find session index");
        Console.WriteLine("6. Check if session exists");
        Console.WriteLine("7. Show duration statistics");
        Console.WriteLine("8. Show session date details");
        Console.WriteLine("9. Show past and upcoming sessions");
        Console.WriteLine("10. Find next session");
        Console.WriteLine("11. Compare two session dates");
        Console.WriteLine("12. Read and validate a custom date");
        Console.WriteLine("13. Select session by index");
        Console.WriteLine("14. Validate session duration");
        Console.WriteLine("15. Generate report using string");
        Console.WriteLine("16. Generate report using StringBuilder");
        Console.WriteLine("0. Exit");

        Console.Write("Choose an option: ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            Console.WriteLine();
            continue;
        }

        switch (choice)
        {
            case 1:
                DisplayAllSessions(
                    sessionNames, sessionDates, sessionDurations);
                break;

            case 2:
                FindSession( sessionNames);
                break;

            case 3:
                SortSessionNames(sessionNames);
                break;

            case 4:
                ReverseSessionNames(sessionNames);
                break;

            case 5:
                FindSessionIndex(sessionNames);
                break;

            case 6:
                CheckSessionExists(sessionNames);
                break;

            case 7:
                DisplayDurationAnalysis(sessionDurations);
                break;

            case 8:
                DisplaySessionDateDetails(
                    sessionNames, sessionDates, sessionDurations);
                break;

            case 9:
                DisplaySessionStatuses(sessionNames, sessionDates);
                break;

            case 10:
                FindNextSession(sessionNames, sessionDates, sessionDurations);
                break;

            case 11:
                CalculateSessionDateDifference( sessionNames, sessionDates);
                break;

            case 12:
                DateTime customDate = ReadAndValidateDate();
                Console.WriteLine(
                    $"Valid date: {customDate:yyyy-MM-dd HH:mm}");
                break;

            case 13:
                DisplaySessionByIndex(sessionNames);
                break;

            case 14:
                Console.Write("Enter duration: ");

                if (int.TryParse(Console.ReadLine(), out int duration))
                {
                    try
                    {
                        ValidateSessionDuration(duration);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid duration. Enter a number.");
                }

                break;

            case 15:
                Console.WriteLine(
                    BuildReportUsingString(
                        sessionNames, sessionDates, sessionDurations));
                break;

            case 16:
                Console.WriteLine(
                    BuildReportUsingStringBuilder(
                        sessionNames, sessionDates, sessionDurations));
                break;

            case 0:
                isRunning = false;
                Console.WriteLine("Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid option. Please choose 0-16.");
                break;
        }

        Console.WriteLine();
    }







//BenchmarkRunner.Run<StringBenchmark>();

static void DisplayAllSessions(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations)
{
    for (int i = 0; i < sessionNames.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {sessionNames[i]}");

        Console.WriteLine(
            $"Date: {sessionDates[i].ToString("dd MMMM yyyy")}");

        Console.WriteLine(
            $"Start Time: {sessionDates[i].ToString("hh:mm tt")}");

        Console.WriteLine(
            $"Duration: {sessionDurations[i]} minutes");

        Console.WriteLine();
    }
}




static void SortSessionNames(string[] sessionNames)
{
    string[] sortedNames = new string[sessionNames.Length];

    Array.Copy(sessionNames, sortedNames, sessionNames.Length);

    Array.Sort(sortedNames);

    Console.WriteLine();

    foreach (string name in sortedNames)
    {
        Console.WriteLine(name);
    }
}



static void ReverseSessionNames(string[] sessionNames)
{
    string[] reversedNames = new string[sessionNames.Length];

    Array.Copy(sessionNames, reversedNames, sessionNames.Length);

    Array.Reverse(reversedNames);
    Console.WriteLine();

    foreach (string name in reversedNames)
    {
        Console.WriteLine(name);
    }
}


static void FindSessionIndex(string[] sessionNames)
{

    Console.Write("\nEnter session name: ");
    string searchName = Console.ReadLine() ?? "";


    int index = Array.IndexOf(sessionNames, searchName);

    Console.WriteLine($"Index: {index}");
}


static void CheckSessionExists(string[] sessionNames)
{
    Console.Write("Enter session name: ");
    string searchName = Console.ReadLine() ?? "";

    bool exists = Array.Exists(
        sessionNames,
        name => string.Equals(
            name,
            searchName,
            StringComparison.OrdinalIgnoreCase));

    if (exists)
    {
        Console.WriteLine("Session exists.");
    }
    else
    {
        Console.WriteLine("Session does not exist.");
    }
}


static void FindSession(string[] sessionNames)
{
    Console.Write("\nEnter session name: ");
    string searchName = Console.ReadLine() ?? "";
    string? session = Array.Find(
        sessionNames,
        name => name.Contains(
            searchName,
            StringComparison.OrdinalIgnoreCase));

    Console.WriteLine(session ?? "Session not found.");
}


static void FindSessionIndexByCondition(string[] sessionNames)
{
    int index = Array.FindIndex(
        sessionNames,
        name => name.StartsWith(
            "F"));

    Console.WriteLine($"Index: {index}");
}



static void CopySessionNames(string[] sessionNames)
{
    string[] copiedNames = new string[sessionNames.Length];

    Array.Copy(sessionNames, copiedNames, sessionNames.Length);

    copiedNames[0] = "C# Advanced";

    Console.WriteLine("Original array:");

    foreach (string name in sessionNames)
    {
        Console.WriteLine(name);
    }

    Console.WriteLine("\nCopied array:");

    foreach (string name in copiedNames)
    {
        Console.WriteLine(name);
    }
}


static int CalculateTotalDuration(int[] sessionDurations)
{
    int total = 0;

    for (int i = 0; i < sessionDurations.Length; i++)
    {
        total += sessionDurations[i];
    }

    return total;
}

static double CalculateAverageDuration(int[] sessionDurations)
{
    if (sessionDurations.Length == 0)
    {
        return 0;
    }

    int total = CalculateTotalDuration(sessionDurations);

    return (double)total / sessionDurations.Length;
}

static int FindShortestDuration(int[] sessionDurations)
{
    if (sessionDurations.Length == 0)
    {
        return 0;
    }

    int shortest = sessionDurations[0];

    for (int i = 1; i < sessionDurations.Length; i++)
    {
        if (sessionDurations[i] < shortest)
        {
            shortest = sessionDurations[i];
        }
    }

    return shortest;
}


static int FindLongestDuration(int[] sessionDurations)
{
    if (sessionDurations.Length == 0)
    {
        return 0;
    }

    int longest = sessionDurations[0];

    for (int i = 1; i < sessionDurations.Length; i++)
    {
        if (sessionDurations[i] > longest)
        {
            longest = sessionDurations[i];
        }
    }

    return longest;
}

static void DisplayDurationAnalysis(int[] sessionDurations)
{
    Console.WriteLine(
        $"Total Duration: {CalculateTotalDuration(sessionDurations)} minutes");

    Console.WriteLine(
        $"Average Duration: {CalculateAverageDuration(sessionDurations):0.##} minutes");

    Console.WriteLine(
        $"Shortest Duration: {FindShortestDuration(sessionDurations)} minutes");

    Console.WriteLine(
        $"Longest Duration: {FindLongestDuration(sessionDurations)} minutes");
}


static void DisplaySortedDurations(int[] sessionDurations)
{
    int[] sortedDurations = new int[sessionDurations.Length];

    Array.Copy(
        sessionDurations,
        sortedDurations,
        sessionDurations.Length);

    Array.Sort(sortedDurations);

    Console.WriteLine("Sorted durations:");

    foreach (int duration in sortedDurations)
    {
        Console.WriteLine($"{duration} minutes");
    }
}


static DateTime GetSessionEndTime(
    DateTime sessionDate,
    int durationMinutes)
{
    return sessionDate.AddMinutes(durationMinutes);
}


static DateTime ReadSessionDate()
{
    string[] formats =
    {
        "dd/MM/yyyy HH:mm",
        "yyyy-MM-dd HH:mm"
    };

    while (true)
    {
        Console.Write(
            "Enter date and time (dd/MM/yyyy HH:mm): ");

        string input = Console.ReadLine() ?? "";

        if (DateTime.TryParseExact(
            input,
            formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime result))
        {
            return result;
        }

        Console.WriteLine(
            "Invalid date. Please try again.");
    }
}


static string BuildReportUsingString(
    string[] sessionNames,
    DateTime[] sessionDates,
    int[] sessionDurations)
{
    string report = "ACADEMY SCHEDULE REPORT WITH STRING\n";
    report += "=======================\n\n";

    for (int i = 0; i < sessionNames.Length; i++)
    {
        report += $"{i + 1}. {sessionNames[i]}\n";

        report +=
            $"Date: {sessionDates[i].ToString("dd MMMM yyyy", CultureInfo.InvariantCulture)}\n";

        report +=
            $"Start Time: {sessionDates[i].ToString("hh:mm tt", CultureInfo.InvariantCulture)}\n";

        report +=
            $"Duration: {sessionDurations[i]} minutes\n\n";
    }

    report += $"Total Duration: {CalculateTotalDuration(sessionDurations)} minutes\n";
    report += $"Average Duration: {CalculateAverageDuration(sessionDurations):0.##} minutes\n";
    report += $"Shortest Duration: {FindShortestDuration(sessionDurations)} minutes\n";
    report += $"Longest Duration: {FindLongestDuration(sessionDurations)} minutes\n";

    return report;
}


static string BuildReportUsingStringBuilder(
    string[] sessionNames,
    DateTime[] sessionDates,
    int[] sessionDurations)
{
    StringBuilder report = new StringBuilder();

    report.AppendLine("ACADEMY SCHEDULE REPORT WITH STRINGBUILDER");
    report.AppendLine("========================================");
    report.AppendLine();

    for (int i = 0; i < sessionNames.Length; i++)
    {
        report.AppendLine($"{i + 1}. {sessionNames[i]}");

        report.AppendLine(
            $"Date: {sessionDates[i].ToString("dd MMMM yyyy", CultureInfo.InvariantCulture)}");

        report.AppendLine(
            $"Start Time: {sessionDates[i].ToString("hh:mm tt", CultureInfo.InvariantCulture)}");

        report.AppendLine(
            $"Duration: {sessionDurations[i]} minutes");

        report.AppendLine();
    }

    report.AppendLine(
        $"Total Duration: {CalculateTotalDuration(sessionDurations)} minutes");

    report.AppendLine(
        $"Average Duration: {CalculateAverageDuration(sessionDurations):0.##} minutes");

    report.AppendLine(
        $"Shortest Duration: {FindShortestDuration(sessionDurations)} minutes");

    report.AppendLine(
        $"Longest Duration: {FindLongestDuration(sessionDurations)} minutes");

    return report.ToString();
}


static void IncreaseValue(ref int number)
{
    number += 10;
}


static bool TryGetSessionInfo(
    string[] sessionNames,
    int[] sessionDurations,
    string searchName,
    out int index,
    out int duration)
{
    index = Array.FindIndex(
        sessionNames,
        name => string.Equals(
            name,
            searchName,
            StringComparison.OrdinalIgnoreCase));

    if (index == -1)
    {
        duration = 0;
        return false;
    }

    duration = sessionDurations[index];
    return true;
}


static void ChangeFirstSession(string[] names)
{
    names[0] = "C# Advanced";
}

static int CalculateTotalDurationWithParams(params int[] durations)
{
    int total = 0;

    foreach (int duration in durations)
    {
        total += duration;
    }

    return total;
}

static void DisplaySessionDateDetails(
    string[] sessionNames,
    DateTime[] sessionDates,
    int[] sessionDurations)
{
    Console.Write("Enter session name: ");
    string searchName = Console.ReadLine() ?? "";

    int index = Array.FindIndex(
        sessionNames,
        name => string.Equals(
            name,
            searchName,
            StringComparison.OrdinalIgnoreCase));

    if (index == -1)
    {
        Console.WriteLine("Session not found.");
        return;
    }

    DateTime startDate = sessionDates[index];
    int duration = sessionDurations[index];

    // Calculate the end time dynamically
    DateTime endDate = startDate.AddMinutes(duration);

    Console.WriteLine($"\nSession: {sessionNames[index]}");
    Console.WriteLine(
        $"Date: {startDate.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture)}");
    Console.WriteLine($"Day: {startDate.DayOfWeek}");
    Console.WriteLine($"Year: {startDate.Year}");
    Console.WriteLine($"Month: {startDate.Month}");
    Console.WriteLine($"Day Number: {startDate.Day}");
    Console.WriteLine(
        $"Start Time: {startDate.ToString("hh:mm tt", CultureInfo.InvariantCulture)}");
    Console.WriteLine($"Duration: {duration} minutes");
    Console.WriteLine(
        $"End Time: {endDate.ToString("hh:mm tt", CultureInfo.InvariantCulture)}");
}

static void CalculateSessionDateDifference(
    string[] sessionNames,
    DateTime[] sessionDates)
{
    Console.Write("Enter first session name: ");
    string firstName = Console.ReadLine() ?? "";

    Console.Write("Enter second session name: ");
    string secondName = Console.ReadLine() ?? "";

    int firstIndex = Array.FindIndex(
        sessionNames,
        name => string.Equals(
            name, firstName, StringComparison.OrdinalIgnoreCase));

    int secondIndex = Array.FindIndex(
        sessionNames,
        name => string.Equals(
            name, secondName, StringComparison.OrdinalIgnoreCase));

    if (firstIndex == -1 || secondIndex == -1)
    {
        Console.WriteLine("One or both sessions were not found.");
        return;
    }

    TimeSpan difference =
        (sessionDates[firstIndex] - sessionDates[secondIndex]).Duration();

    Console.WriteLine("\nDifference:");
    Console.WriteLine($"{difference.TotalDays:0.##} days");
    Console.WriteLine($"{difference.TotalHours:0.##} hours");
}


static void DisplaySessionStatuses(
    string[] sessionNames,
    DateTime[] sessionDates)
{
    for (int i = 0; i < sessionNames.Length; i++)
    {
        if (sessionDates[i] < DateTime.Now)
        {
            Console.WriteLine($"{sessionNames[i]} Past");
        }
        else
        {
            Console.WriteLine($"{sessionNames[i]} Upcoming");
        }
    }
}


static void FindNextSession(
    string[] sessionNames,
    DateTime[] sessionDates,
    int[] sessionDurations)
{
    DateTime now = DateTime.Now;
    int nextIndex = -1;

    for (int i = 0; i < sessionDates.Length; i++)
    {
        if (sessionDates[i] > now)
        {
            if (nextIndex == -1 || sessionDates[i] < sessionDates[nextIndex])
            {
                nextIndex = i;
            }
        }
    } 

    if (nextIndex == -1)
    {
        Console.WriteLine("No upcoming sessions found.");
        return;
    }

    DateTime nextDate = sessionDates[nextIndex];
    TimeSpan remaining = nextDate - now;

    Console.WriteLine("\nNext Session:");
    Console.WriteLine(sessionNames[nextIndex]);
    Console.WriteLine(
        nextDate.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture));
    Console.WriteLine(
        nextDate.ToString("hh:mm tt", CultureInfo.InvariantCulture));

    Console.WriteLine("\nTime Remaining:");
    Console.WriteLine($"{(int)remaining.TotalDays} days");
    Console.WriteLine($"{remaining.Hours} hours");
}

static void DisplaySessionDateFormats(
    string[] sessionNames,
    DateTime[] sessionDates)
{
    Console.Write("Enter session name: ");
    string searchName = Console.ReadLine() ?? "";

    int index = Array.FindIndex(
        sessionNames,
        name => string.Equals(
            name,
            searchName,
            StringComparison.OrdinalIgnoreCase));

    if (index == -1)
    {
        Console.WriteLine("Session not found.");
        return;
    }

    DateTime sessionDate = sessionDates[index];

    Console.WriteLine(sessionDate.ToString("yyyy-MM-dd"));
    Console.WriteLine(sessionDate.ToString("dd/MM/yyyy"));
    Console.WriteLine(sessionDate.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture));
    Console.WriteLine(sessionDate.ToString("dddd, dd MMMM yyyy", CultureInfo.InvariantCulture));
    Console.WriteLine(sessionDate.ToString("hh:mm tt", CultureInfo.InvariantCulture));
}

static DateTime ReadAndValidateDate()
{
    while (true)
    {
        Console.Write("Enter date and time (yyyy-MM-dd HH:mm): ");
        string input = Console.ReadLine() ?? "";

        bool isValid = DateTime.TryParseExact(
            input,
            "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime result);

        if (isValid)
        {
            return result;
        }

        Console.WriteLine("Invalid date or time. Please try again.");
    }
}


static void DisplaySessionByIndex(string[] sessionNames)
{
    Console.Write("Enter session index: ");
    string input = Console.ReadLine() ?? "";

    try
    {
        bool flag = int.TryParse(input, out int index);
        if (flag)
        {
            Console.WriteLine($"Session: {sessionNames[index]}");
        }
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("The selected session index is out of range.");
    }
}

static void ValidateSessionDuration(int duration)
{
    if (duration <= 0)
    {
        throw new ArgumentException(
            "Duration must be greater than zero.");
    }

    Console.WriteLine("Duration accepted.");
}