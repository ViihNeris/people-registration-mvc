using Microsoft.AspNetCore.Mvc;
using PeopleRegistration2.Models;
using PeopleRegistration2.Repository;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PeopleRegistration2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }

        [Filtros.LogFilter]
        public IActionResult Index()
        {
            var usersList = userRepository.Listar();
            return View(usersList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public ActionResult Create(Models.UserModel userModel)
        {
            if (ModelState.IsValid)
            {
               userRepository.Insert(userModel);

                @TempData["mensagem"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View(userModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var user = userRepository.Query(Id);
            return View(user);
        }
        
        [HttpPost]
        public ActionResult Edit(Models.UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                userRepository.Update(userModel);

                @TempData["mensagem"] = "Usuário atualizado com sucesso.";
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View(userModel);
            }
        }

        [HttpGet]
        public ActionResult Query(int Id)
        {
            var user = userRepository.Query(Id);
            return View(user);
        }

        public ActionResult DeleteConfirm(int Id)
        {
            var user = userRepository.Query(Id);
            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            userRepository.Delete(Id);
            @TempData["mensagem"] = "Usuário removido com sucesso.";
            return RedirectToAction("Index", "User");
        }
    }
}
