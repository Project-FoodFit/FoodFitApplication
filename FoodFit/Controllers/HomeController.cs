using FoodFit.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace FoodFit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Global.isUserJustLogged)
            {
                ViewBag.AlertMessage = $"You has entered as: {Global.currentUserEmail}";
                Global.isUserJustLogged = false;
            }
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Users modelRegistration)
        {
            if (modelRegistration.Name == null || modelRegistration.Surname == null || modelRegistration.Patronymic == null ||
                modelRegistration.Email == null || modelRegistration.Password == null)
            {
                ViewData["ValidateMessage"] = "Не были введены все необходимые поля";
                return View("Index2");
            }
            SqlConnection myConnection = new SqlConnection(Global.dbConnection);
            // Проверка на существование пользователя с введенной электронной почтой
            myConnection.Open();
            string selectquery = $"select * from Users where Email = '{modelRegistration.Email}'";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            myConnection.Close();
            if (table.Rows.Count > 0)
            {
                ViewData["ValidateMessage"] = "Пользователь с данной электронной почтой уже существует";
                return View("Index2");
            }
            // Проверка на корректность пароля
            Regex passwordCheck = new Regex(@"^\w{5,15}$");
            MatchCollection  matches = passwordCheck.Matches(modelRegistration.Password);
            if (matches.Count == 0)
            {
                ViewData["ValidateMessage"] = "Некорректно введен пароль";
                return View("Index2");
            }
            // Проверка на корректность фамилии
            Regex nameCheck = new Regex(@"^[A-ЯЁ][а-яё]+$");
            matches = nameCheck.Matches(modelRegistration.Surname);
            if (matches.Count == 0)
            {
                ViewData["ValidateMessage"] = "Неверно введена фамилия";
                return View("Index2");
            }
            // Проверка на корректность имени
            matches = nameCheck.Matches(modelRegistration.Name);
            if (matches.Count == 0)
            {
                ViewData["ValidateMessage"] = "Неверно введено имя";
                return View("Index2");
            }
            // Проверка на наличие и корректность отчества
            if (modelRegistration.Patronymic == null)
            {
                modelRegistration.Patronymic = "NULL";
            }
            else
            {
                matches = nameCheck.Matches(modelRegistration.Patronymic);
                if (matches.Count == 0)
                {
                    ViewData["ValidateMessage"] = "Неверно введено отчество";
                    return View("Index2");
                }
                else
                {
                    modelRegistration.Patronymic = $"'{modelRegistration.Patronymic}'";
                }
            }
            myConnection.Open();
            // Добавление пользователя в базу данных
            selectquery = $"insert into Users (Surname, Name, Patronymic, Email, Password, RoleID) " +
                $"values ('{modelRegistration.Surname}', '{modelRegistration.Name}', {modelRegistration.Patronymic}, '{modelRegistration.Email}', '{modelRegistration.Password}', 2)";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            myConnection.Close();
            ViewData["ValidateMessage"] = "Пользователь был успешно создан";
            return View("Index2");
        }
        [HttpPost]
        public async Task<IActionResult> Login(Users modelLogin)
        {
            // Проверка на заполнение полей
            if (modelLogin.Email == null && modelLogin.Password == null)
            {
                ViewData["ValidateMessage"] = "Ошибка. Не были введены данные";
            }
            else
            {
                // Проверка на существование пользователя с введенной электронной почтой
                SqlConnection myConnection = new SqlConnection(Global.dbConnection);
                myConnection.Open();
                string selectquery = $"select Email, Password, RoleID from Users where Email = '{modelLogin.Email}'";
                SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
                DataTable table = new DataTable();
                adpt.Fill(table);
                myConnection.Close();
                if (table.Rows.Count > 0)
                {
                    // Проверка на совпадение введенных электронной почты и пароля с данными из бд
                    // Вход для администатора
                    myConnection.Open();
                    selectquery = $"select Email, Password, RoleID from Users where Email = '{modelLogin.Email}' and Password = '{modelLogin.Password}' and RoleID = 1";
                    adpt = new SqlDataAdapter(selectquery, myConnection);
                    table = new DataTable();
                    adpt.Fill(table);
                    myConnection.Close();
                    if (table.Rows.Count > 0)
                    {
                        Global.currentUserEmail = modelLogin.Email;
                        Global.isUserJustLogged = true;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["ValidateMessage"] = "Неверный пароль";
                    }
                    // Вход для клиента
                    myConnection.Open();
                    selectquery = $"select Email, Password, RoleID from Users where Email = '{modelLogin.Email}' and Password = '{modelLogin.Password}' and RoleID = 2";
                    adpt = new SqlDataAdapter(selectquery, myConnection);
                    table = new DataTable();
                    adpt.Fill(table);
                    myConnection.Close();
                    if (table.Rows.Count > 0)
                    {
                        Global.currentUserEmail = modelLogin.Email;
                        Global.isUserJustLogged = true;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["ValidateMessage"] = "Неверный пароль";
                    }
                }
                else
                {
                    ViewData["ValidateMessage"] = "Пользователя с таким логином не существует";
                }
            }
            return View("Index2");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}