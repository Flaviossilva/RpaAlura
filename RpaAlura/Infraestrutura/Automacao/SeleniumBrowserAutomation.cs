using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RpaAlura.Dominio.Entidades;
using RpaAlura.Dominio.Servicos;
using SeleniumExtras.WaitHelpers;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace RpaAlura.Infraestrutura.Automacao
{
    public class SeleniumBrowserAutomation : IBrowserAutomation
    {
        private readonly IWebDriver _driver;

        public SeleniumBrowserAutomation(IWebDriver driver)
        {
            _driver = driver;
        }

        public List<Course> SearchAsync(string term)
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
            List<Course> ListModelCouse = new List<Course>();
            Thread.Sleep(1000
            ListModelCouse = RasparPagina(_driver);
            do
            {
                var buttonNextPage = EsperarElemento(_driver, "CssSelector", "#busca > nav > a.busca-paginacao-prevNext.busca-paginacao-linksProximos");
                if (buttonNextPage is not null)
                {
                    var isDisabled = buttonNextPage.GetAttribute("class").Contains("busca-paginacao-prevNext--disabled");
                    if (isDisabled)
                        break;
                    buttonNextPage.Click();
                    bool iscli = IsElementClickable(_driver, By.XPath("/html/body/div[2]/div[2]/nav/a[2]"));
                    Thread.Sleep(500);
                    ListModelCouse.AddRange(RasparPagina(_driver));
                }
            } while (true);
            return ListModelCouse;
        }

        public bool IsElementClickable(IWebDriver driver, By by)
        {
            try
            {
                // Define um tempo máximo de espera para que o elemento seja clicável
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Aguarda até que o elemento esteja clicável
                IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(by));

                // Se não houver exceções, o elemento é clicável
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                // Caso o elemento não se torne clicável no tempo estipulado
                return false;
            }
        }

        public static List<Course> RasparPagina(IWebDriver _driver)
        {
            List<Course> ListModelCouse = new List<Course>();
            Actions actions = new Actions(_driver);
            var TableCourse = _driver.FindElement(By.XPath("/html/body/div[2]/div[2]/section/ul"));
            var tableRow = TableCourse.FindElements(By.TagName("li"));
            foreach (IWebElement row1 in tableRow)
            {
                IWebElement searchProfessor = null;
                var xpaths = new[] { "/html/body/section[2]/div[1]/section/div/div/div/h3", "/html/body/section[2]/div[1]/section/div/div/div[2]/div/div/h3" };
                actions.SendKeys(OpenQA.Selenium.Keys.Down).Build().Perform();
                actions.SendKeys(OpenQA.Selenium.Keys.Down).Build().Perform();
                row1.Click();
                Course ModelCouse = new Course();
                List<string> listDescription = new List<string>();
                ModelCouse.Title = _driver.FindElement(By.XPath("/html/body/section[1]/div/div[1]/h1")).Text;
                ModelCouse.Duration = _driver.FindElement(By.XPath("/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]")).Text;
                var TableDescription = _driver.FindElement(By.XPath("/html/body/section[2]/div[1]/div/ul"));
                var TableDescriptionRow = TableDescription.FindElements(By.XPath("li"));
                foreach (IWebElement Tabledescriptionrow in TableDescriptionRow)
                {
                    listDescription.Add(Tabledescriptionrow.Text);
                }
                ModelCouse.Description = string.Join(" ", listDescription);
                ModelCouse.Description = TruncarTexto(ModelCouse.Description, 500);
                ModelCouse.Description = RemoverAcentuacaoEAspas(ModelCouse.Description);
                foreach (var xpath in xpaths)
                {
                    searchProfessor = EsperarElemento(_driver, "XPath", xpath);
                    if (searchProfessor is not null)
                        break;
                }
                ModelCouse.Professor = searchProfessor is not null
                    ? searchProfessor.Text
                    : "Curso sem professor na página";

                ListModelCouse.Add(ModelCouse);
                _driver.Navigate().Back();
                Thread.Sleep(1400);
            }
            _driver.Quit();
            return ListModelCouse;
        }
        public static string RemoverAcentuacaoEAspas(string texto)
        {
            // Remove acentuação utilizando a normalização Unicode
            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in textoNormalizado)
            {
                // Mantém apenas caracteres que não sejam marcas de acentuação
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // Remove aspas simples e aspas duplas usando Regex
            string resultado = sb.ToString();
            resultado = Regex.Replace(resultado, @"['""]", "");

            // Retorna o texto sem acentuação e sem aspas
            return resultado.Normalize(NormalizationForm.FormC); // Retorna à forma normal
        }
        public static string TruncarTexto(string texto, int tamanhoMaximo)
        {
            if (texto.Length > tamanhoMaximo)
            {
                texto = texto.Substring(0, tamanhoMaximo);
            }
            return texto;
        }
        public static IWebElement? EsperarElemento(IWebDriver drive, string by, string element)
        {
            //Metodo responsavel por buscar elementos no site, caso não encontrar tentar 3x e tratar erro.
            IWebElement? Element = null;
            for (int NTentativas = 0; NTentativas < 3; NTentativas++)
            {
                try
                {
                    if (drive != null)
                    {
                        Thread.Sleep(100);
                        if (by.Equals("Id"))
                            Element = drive.FindElement(By.Id(element));
                        if (by.Equals("XPath"))
                            Element = drive.FindElement(By.XPath(element));
                        if (by.Equals("ClassName"))
                            Element = drive.FindElement(By.ClassName(element));
                        if (by.Equals("LinkText"))
                            Element = drive.FindElement(By.LinkText(element));
                        if (by.Equals("CssSelector"))
                            Element = drive.FindElement(By.CssSelector(element));
                        if (by.Equals("Name"))
                            Element = drive.FindElement(By.Name(element));
                        if (by.Equals("PartialLinkText"))
                            Element = drive.FindElement(By.PartialLinkText(element));
                    }
                    return Element;
                }
                catch (Exception)
                {
                    Thread.Sleep(200);
                }
            }
            return null;
        }

    }
}
