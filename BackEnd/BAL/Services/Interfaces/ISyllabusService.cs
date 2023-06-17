using BAL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DAL.Entities.Syllabus;

namespace BAL.Services.Interfaces
{
    public interface ISyllabusService
    {
        //team01
        void DeactivateSyllabus(long id, List<Claim> claims);
        void ActivateSyllabus(long id, List<Claim> claims);
        SyllabusViewModel GetById(long id);
        void DuplicateSyllabus(long id, List<Claim> claims);
        public void CreateSyllabus(SyllabusViewModel syllabus, List<Claim> claims);
        public void SaveAsDraft(SyllabusViewModel syllabus, List<Claim> claims);
        void UpdateSyllabus(SyllabusViewModel syllabusViewModel, List<Claim> claims);
        public void DeleteSyllabus(long id, List<Claim> claims);
        public void SetSaveAsDraftToCreateCheck(bool check);
        //team01
        public List<SyllabusModel> GetAll(List<string>? key, int PAGE_SIZE, DateTime? from, DateTime? to, List<string>? sortBy, int page );
        //List<SyllabusViewModel> GetAll(List<string>? key, int PAGE_SIZE, DateTime? from, DateTime? to, string? sortBy, int page = 1);       
        public Task<UpLoadExcelFileResponse> UploadExcelFile(UpLoadExcelFileRequest request, string path);


        int GetDuration(Syllabus syl);
        DateTime GetCreatedOn(Syllabus syllabus);
        string GetCreatedBy(Syllabus syllabus);
        List<SyllabusModel> ShowSyllabuses(List<Syllabus> syllabuses);
        // Team6
        TrainingProgramDetailViewModel GetDetailTrainingProgram(long id);
        List<SearchSyllabusViewModel> SearchSyllabusByName(string name);
        // Team6
        void Save();
        void SaveAsync();
    }
}
