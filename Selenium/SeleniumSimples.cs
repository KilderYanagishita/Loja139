// namespace ~ pacote ~grupos de classes ~ workspace 

namespace SeleniumSimples;

// Bibliotecas ~ Dependencias
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

// Classe
[TestFixture] //Configura como uma Classe de Teste
public class AdicionarProdutoNoCarrinhoTest{
    // Atributos ~ Caracteristicas ~ Campos
    private IWebDriver driver; //Objeto do Selenium WebDriver
    // Funçao ou Metodo de Apoio

    // Finçao de Leitura do arquivo csv - massa de teste
    public static IEnumerable<TestCaseData> lerDadosDeTeste(){
        // declaramos um objeto chamado reader que le o conteudo do csv
        using (var reader = new StreamReader(@"C:\Iterasys\Loja139\data\login.csv")){
            //Pular a linha do cabeçalho do csv
            reader.ReadLine();

            //Faça enquanto noa for o final do arquivos
            while (!reader.EndOfStream){
                // Ler a linha correspondente
                var linha = reader.ReadLine();
                var valores = linha.Split(", ");

                yield return new TestCaseData(valores[0],valores[1],valores[2]);
                } // Fim do While
        };
    }

    // Configuraçoes de Antes do Teste
    [SetUp] //Configura uma Metodo para ser Executado antes dos testes
    public void Before(){
        new DriverManager().SetUpDriver(new ChromeConfig()); //Faz o Download e instalação da versão do CHromeDriver
        driver = new ChromeDriver(); //Liga o Chrome Driver
        driver.Manage().Window.Maximize(); //Maximiza a Janela do Navegador
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5000); // Configura o tempo que vamos esperar 2 segundos
    }// Fim do Before
    // Configuraçoes de Depois do Teste
    [TearDown] // Configura o Metodo para ser usado depois do teste
    public void After(){
        driver.Quit();
    } // Fim After

    // Test
    [Test] // Inicia o Metodo Test
    public void Login(){
        // Abrir o Navegador e Acessar o Site
        driver.Navigate().GoToUrl("https://www.saucedemo.com/"); 
        Thread.Sleep(2000); //Pausa Forçada       
        // Preencher Usuario
        driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
        Thread.Sleep(2000); //Pausa Forçada
        // Preencher Senha
        driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
        Thread.Sleep(2000); //Pausa Forçada
        // Clicar Login
        driver.FindElement(By.Id("login-button")).Click();
        Thread.Sleep(2000); //Pausa Forçada
        // Validar se fizemos o login no sistema, confirmando o texto ancora
        Is.Equals(driver.FindElement(By.CssSelector("span.title")).Text, "Products");
        Thread.Sleep(2000); //Pausa Forçada  
        // Adicionar ao Carrinho
        driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
        Thread.Sleep(2000); //Pausa Forçada
        // Abrir o Carrinho
        driver.FindElement(By.Id("shopping_cart_container")).Click();
        Thread.Sleep(2000); //Pausa Forçada
        // Verificar se o produto esta no carrinho
        Is.Equals(driver.FindElement(By.CssSelector("span.title")).Text, "Sauce Labs Backpack");
        Thread.Sleep(2000); //Pausa Forçada   
         // Clicar em Remove
        driver.FindElement(By.Id("remove-sauce-labs-backpack")).Click();
        Thread.Sleep(2000); //Pausa Forçada   

    }
[Test, TestCaseSource("lerDadosDeTeste")] // Inicia o Metodo Test
    public void LoginPositivoDDT(string Usuario, string Senha, string resultadoEsperado){
        // Abrir o Navegador e Acessar o Site
        driver.Navigate().GoToUrl("https://www.saucedemo.com/"); 
        Thread.Sleep(2000); //Pausa Forçada       
        // Preencher Usuario
        driver.FindElement(By.Id("user-name")).SendKeys(Usuario);
        Thread.Sleep(2000); //Pausa Forçada
        // Preencher Senha
        driver.FindElement(By.Id("password")).SendKeys(Senha);
        Thread.Sleep(2000); //Pausa Forçada
        // Clicar Login
        driver.FindElement(By.Id("login-button")).Click();
        Thread.Sleep(2000); //Pausa Forçada
        // Validar se fizemos o login no sistema, confirmando o texto ancora
        Is.Equals(driver.FindElement(By.CssSelector("span.title")).Text, resultadoEsperado);
        Thread.Sleep(2000); //Pausa Forçada        

}
} //Fim da Classe
