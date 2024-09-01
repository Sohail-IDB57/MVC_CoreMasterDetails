using CoreMasterDetails.Data;
using CoreMasterDetails.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using System.Threading.Tasks;

namespace CoreMasterDetails.Controllers
{
    public class MasterDetailsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public MasterDetailsController(MyDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
    
        public IActionResult Index()
        {
            List<Applicant> applicants;
            applicants = _context.Applicants.ToList();
            return View(applicants);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            Applicant applicant = new Applicant();
            applicant.Experiences.Add(new Experience() { ExperienceId = 1 });
            return View(applicant);
        }
        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {
            applicant.Experiences.RemoveAll(n=> n.YearsWorked==0);  
            string uniqueFileName= GetUploadedFileName(applicant);
            applicant.PhotoUrl= uniqueFileName;
            _context.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;
            if (applicant.ProfilePhoto != null)
            {
                string uploadFolder = Path.Combine(_webHost.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var applicant = _context.Applicants
        .Include(a => a.Experiences)
        .FirstOrDefault(a => a.Id == id);

            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Applicant applicant, string DeletedExperiences)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (applicant.ProfilePhoto != null)
                    {
                        string uniqueFileName = GetUploadedFileName(applicant);
                        applicant.PhotoUrl = uniqueFileName;
                    }

                    _context.Attach(applicant);
                  
                    if (!string.IsNullOrEmpty(DeletedExperiences))
                    {
                        var idsToDelete = DeletedExperiences.Split(',')
                            .Where(id => int.TryParse(id, out _))
                            .Select(int.Parse)
                            .ToList();

                        foreach (var id in idsToDelete)
                        {
                            var experience = await _context.Experiences.FindAsync(id);
                            if (experience != null)
                            {
                                _context.Experiences.Remove(experience);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }

                    foreach (var experience in applicant.Experiences)
                    {
                        if (experience.ExperienceId == 0)
                        {
                            _context.Entry(experience).State = EntityState.Added;
                        }
                        else
                        {
                            _context.Entry(experience).State = EntityState.Modified;
                        }
                    }

                    _context.Entry(applicant).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Applicants.Any(a => a.Id == applicant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {                      
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(applicant);
        }


        public IActionResult Details(int id)
        {
            Applicant applicant= _context.Applicants
                .Include(e=>e.Experiences)
                .Where(a=>a.Id==id)
                .FirstOrDefault();
            return View(applicant);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Applicant applicant = _context.Applicants
               .Include(e => e.Experiences)
               .Where(a => a.Id == id)
               .FirstOrDefault();
            return View(applicant);
        }
        [HttpPost]
        public IActionResult Delete(Applicant applicant)
        {
            _context.Attach(applicant);          
            _context.Entry(applicant).State=EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
