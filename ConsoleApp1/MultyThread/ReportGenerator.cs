namespace MultyThread
{
    public class ReportGenerator
    {
        public static int UserCount = 0;

        public static object lockObj = new object();

        private int _count = 0;
        private bool _isActive = false;

        public async void GenerateAllRepots()
        {
            //BuildDaylyReport();
            //BuildMonthlyReport();

            var taskReportMonth = new Task(() => BuildMonthlyReport());
            var taskReportDay = new Task(() => BuildDaylyReport());

            taskReportDay.Start();
            taskReportMonth.Start();

            await Task.WhenAll(taskReportMonth, taskReportDay);
        }

        public void BuildMonthlyReport()
        {

            if (!_isActive)
            {
                _isActive = true;
                // AAA sleep
                WriteToFile("Generate report");
                _isActive = false;
            }
        }

        public void BuildDaylyReport()
        {
            if (!_isActive)
            {
                _isActive = true;

                WriteToFile("Generate report");

                _isActive = false;
            }
        }

        private void WriteToFile(string message)
        {
            // all thread wait here

            lock (lockObj) // Is already at least one thread in the lock?
            {
                // Mock to write to file
                Console.WriteLine(message);
                Console.WriteLine(message);
                Console.WriteLine(message);
                Console.WriteLine(message);
                Console.WriteLine(message);
            }


        }

        public string GenerateReportWithDataFromApiSYNC()
        {
            var http = new HttpClient();
            // Send Requsets
            var answer = http.GetAsync("coolApi.com/getData");
            var asnwerString = answer
                .Result // 10 second
                .Content
                .ReadAsStream()
                .ToString();
            return asnwerString;
        }

        public async Task<string> GenerateReportWithDataFromApiAsync()
        {
            var http = new HttpClient();
            // Send Requsets
            Task<HttpResponseMessage> taskAnswer = http.GetAsync("coolApi.com/getData");
            HttpResponseMessage answerResult = await taskAnswer;// delimeter 10 second

            var asnwerString = answerResult.Content
                .ReadAsStream()
                .ToString();

            return asnwerString;
        }
    }
}
