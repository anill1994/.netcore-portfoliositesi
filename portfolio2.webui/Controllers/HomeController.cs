using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using portfolio.data.Abstract;
using portfolio.webui.Models;

namespace portfolio.webui.Controllers
{
    public class HomeController : Controller
    {
        private IAboutRepository _aboutRepository;
        private IResumeRepository _resumeRepository;
        private ISkillRepository _skillRepository;
        public HomeController(IAboutRepository aboutRepository,
        IResumeRepository resumeRepository,
        ISkillRepository skillRepository
        )
        {
            _aboutRepository = aboutRepository;
            _resumeRepository = resumeRepository;
            _skillRepository = skillRepository;
        }
        public IActionResult Index()
        {
            return View(new IndexListViewModel(){
                About = _aboutRepository.GetOne(),
                Skills = _skillRepository.GetAll(),
                Resumes = _resumeRepository.GetAll()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
