using Microsoft.AspNetCore.Mvc;
using portfolio.entity;
using portfolio.webui.Models;
using portfolio.data.Abstract;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace portfolio.webui.Controllers
{
    [Authorize]
    public class AdminController:Controller
    {
        private IContactRepository _contactRepository;
        private IResumeRepository _resumeRepository;
        private IAboutRepository _aboutRepository;
        private ISkillRepository _skillRepository;
        public AdminController(IContactRepository contactRepository,
        IResumeRepository resumeRepository,
        IAboutRepository aboutRepository,
        ISkillRepository skillRepository
        )
        {
            _contactRepository = contactRepository;
            _resumeRepository = resumeRepository;
            _aboutRepository = aboutRepository;
            _skillRepository = skillRepository;
        }
         public IActionResult ContactList()
         {
            return View(new ContactListViewModel(){
               Contacts = _contactRepository.GetAll()
            });
        }
      
        [HttpPost]
        public IActionResult ContactCreate(ContactModel model){
             if(ModelState.IsValid)
            {
                var entity = new Contact()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Subject = model.Subject,
                    Message = model.Message,
                };
                
              _contactRepository.Create(entity);
                return RedirectToAction("Index","Home");
        }
        return View(model);
    }
    [HttpPost]
    public IActionResult ContactDelete(int id){
        var entity = _contactRepository.GetById((int)id);
        if(entity==null){
            return NotFound();
        }
        _contactRepository.Delete(entity);
        return RedirectToAction("contactlist");
    }
    public IActionResult ResumeList(){
        return View(new ResumeListViewModel(){
          Resumes = _resumeRepository.GetAll()
        });
    }
    public IActionResult ResumeCreate(){

        return View();
    }
    [HttpPost]
      public IActionResult ResumeCreate(ResumeModel model){
           if(ModelState.IsValid)
            {
                var entity = new Resume()
                {
                    Title = model.Title,
                    Company = model.Company,
                    Description = model.Description,
                    Job = model.Job,
                    StartDate = model.StartDate,
                    FinishDate = model.FinishDate
                };
                
              _resumeRepository.Create(entity);
                return RedirectToAction("ResumeList");
            }
        return View();
    }
    public IActionResult ResumeUpdate(int? id){
        if(id==null){
            return NotFound();
        }
        var entity = _resumeRepository.GetById((int)id);
        if(entity==null){
            return NotFound();
        }
        var model = new ResumeModel(){
            ResumeId = entity.ResumeId,
            Title = entity.Title,
            Company = entity.Company,
            Description = entity.Description,
            Job = entity.Job,
            StartDate = entity.StartDate,
            FinishDate = entity.FinishDate
        };
        return View(model);
    }
    [HttpPost]
    public IActionResult ResumeUpdate(ResumeModel model)
    {
        if(ModelState.IsValid){
            var entity = _resumeRepository.GetById(model.ResumeId);
            if(entity==null){
               return NotFound();
            }
             entity.Title = model.Title;
             entity.Company = model.Company;
             entity.Description = model.Description;
             entity.Job = model.Job;
             entity.StartDate = model.StartDate;
             entity.FinishDate = model.FinishDate;
             _resumeRepository.Update(entity);
             return RedirectToAction("resumelist");
        }
        return View(model);
    }
    [HttpPost]
    public IActionResult ResumeDelete(int? resumeId){
        if(resumeId==null){
            return NotFound();
        }
        var entity = _resumeRepository.GetById((int)resumeId);
        if(entity==null){
            return NotFound();
        }
        _resumeRepository.Delete(entity);
        return RedirectToAction("resumelist");
    }
     public IActionResult AboutList(){
        return View(new AboutListViewModel(){
          Abouts = _aboutRepository.GetAll()
        });
    }
    public IActionResult AboutCreate(){
        return View();
    }
    [HttpPost]
      public IActionResult AboutCreate(AboutModel model){
           if(ModelState.IsValid)
            {
                var entity = new About()
                {
                    Title = model.Title,
                    Description = model.Description,
                    FullName = model.FullName,
                    Image = model.Image,
                    Website = model.Website,
                    Email = model.Email,
                    BirthDay = model.BirthDay,
                    Job = model.Job,
                };
                
              _aboutRepository.Create(entity);
                return RedirectToAction("AboutList");
            }
        return View(model);
    }
        public IActionResult AboutUpdate(int? id){
        if(id==null){
            return NotFound();
        }
        var entity = _aboutRepository.GetById((int)id);
        if(entity==null){
            return NotFound();
        }
        var model = new AboutModel(){
            AboutId = entity.AboutId,
            Title = entity.Title,
            Description = entity.Description,
            Image = entity.Image,
            FullName = entity.FullName,
            Website = entity.Website,
            Email = entity.Email,
            BirthDay = entity.BirthDay,
            Job = entity.Job
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> AboutUpdate(AboutModel model,IFormFile file){
        if(ModelState.IsValid){
            var entity = _aboutRepository.GetById(model.AboutId);
            if(entity==null){
                return NotFound();
            }
            entity.AboutId = model.AboutId;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.FullName = model.FullName;
            entity.Website = model.Website;
            entity.Email = model.Email;
            entity.BirthDay = model.BirthDay;
            entity.Job = model.Job; 
            if(file!=null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",randomName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _aboutRepository.Update(entity);
            return RedirectToAction("aboutlist");

        }
        return View(model);
    }
    [HttpPost]
    public IActionResult AboutDelete(int id){
        var entity = _aboutRepository.GetById((int)id);
        if(entity==null){
            return NotFound();
        }
        _aboutRepository.Delete(entity);
        return RedirectToAction("aboutlist");
    }
     public IActionResult SkillList(){
        return View(new SkillListViewModel(){
          Skills = _skillRepository.GetAll()
        });
    }
    public IActionResult SkillCreate(){

        return View();
    }
    [HttpPost]
      public IActionResult SkillCreate(SkillModel model){
           if(ModelState.IsValid)
            {
                var entity = new Skill()
                {
                    SkillName = model.SkillName,
                    SkillRate = model.SkillRate,
                };
              _skillRepository.Create(entity);
                return RedirectToAction("SkillList");
            }
        return View(model);
    }
    public IActionResult SkillUpdate(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var entity = _skillRepository.GetById((int)id);

            if(entity==null)
            {
                 return NotFound();
            }

            var model = new SkillModel()
            {
                SkillId = entity.SkillId,
                SkillName = entity.SkillName,
                SkillRate = entity.SkillRate,
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult SkillUpdate(SkillModel model)
        {
            if(ModelState.IsValid)
            {
                var entity = _skillRepository.GetById(model.SkillId);
                if(entity==null)
                {
                    return NotFound();
                }
                entity.SkillName = model.SkillName;
                entity.SkillRate = model.SkillRate;

                _skillRepository.Update(entity);

                return RedirectToAction("Skilllist");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult SkillDelete(int skillid)
        {
            var entity = _skillRepository.GetById(skillid);

            if(entity!=null)
            {
                _skillRepository.Delete(entity);
                 return RedirectToAction("skilllist");
            }
            return NotFound();
        }
}
}