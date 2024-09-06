using OpenQA.Selenium;
using RpaAlura.Dominio.Entidades;
using RpaAlura.Dominio.Servicos;

namespace RpaAlura.Infraestrutura.Automacao
{
    public class SeleniumBrowserAutomation : IBrowserAutomation
    {
        private readonly IWebDriver _driver;

        public SeleniumBrowserAutomation(IWebDriver driver)
        {
            _driver = driver;
        }

        public async Task SearchAsync(string term)
        {
            _driver.Navigate().GoToUrl("https://www.alura.com.br/");
            Thread.Sleep(1000);
            var searchBox = _driver.FindElement(By.XPath("/html/body/main/section[1]/header/div/nav/div[2]/form/input"));
            Thread.Sleep(1000);
            var ButonFilter = _driver.FindElement(By.XPath("/html/body/main/section[1]/header/div/nav/div[2]/form/input"));
            Thread.Sleep(1000);
            ButonFilter.Click();
            searchBox.SendKeys(term);
            searchBox.Submit();
            var ButtonCourse = _driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/span"));
            Thread.Sleep(1000);
            ButtonCourse.Click();
            var filter = _driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/ul/li[1]/label"));
            filter.Click();
            var ButtonFilterResult = _driver.FindElement(By.XPath("/html/body/div[2]/div[1]/button"));
            Thread.Sleep(1000);
            ButtonFilterResult.Click();
            var TableCourse = _driver.FindElement(By.XPath("/html/body/div[2]/div[2]/section/ul"));
            var tableRow = TableCourse.FindElements(By.TagName("li"));
            foreach (IWebElement row1 in tableRow)
            {
                row1.Click();
                var rowTD = row1.FindElements(By.TagName("a"));
                //Raspagem de dados

            }
        }
        public async Task<List<Course>> GetSearchResultsAsync()
        {
            var courses = new List<Course>();
            var courseElements = _driver.FindElements(By.CssSelector(".course-card"));

            foreach (var courseElement in courseElements)
            {
                var title = courseElement.FindElement(By.CssSelector(".course-title")).Text;
                var professor = courseElement.FindElement(By.CssSelector(".course-author")).Text;
                var duration = courseElement.FindElement(By.CssSelector(".course-duration")).Text;
                var description = courseElement.FindElement(By.CssSelector(".course-description")).Text;


                var t = "teste";
                var p = "teste";
                var du = "teste";
                var de = "teste";

                courses.Add(new Course
                {
                    Title = t,
                    Professor = p,
                    Duration = du,
                    Description = de
                });

                courses.Add(new Course
                {
                    Title = title,
                    Professor = professor,
                    Duration = duration,
                    Description = description
                });
            }

            return courses;
        }
    }
}
