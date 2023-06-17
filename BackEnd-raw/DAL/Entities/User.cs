using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public byte[]? Image { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string? ResetPasswordOtp { get; set; }
        public long IdRole { get; set; }
        public virtual Role Role { get; set; }

        public virtual IEnumerable<HistoryMaterial> HistoryMaterials { get; set; }

        public virtual IEnumerable<SyllabusTrainer> SyllabusTrainers { get; set; }

        public virtual IEnumerable<HistorySyllabus> HistorySyllabi { get; set; }

        public virtual IEnumerable<HistoryTrainingProgram> HistoryTrainingPrograms { get; set; }

        public virtual IEnumerable<Class> CreatedClasses { get; set; }

        public virtual IEnumerable<Class> ReviewedClasses { get; set; }
        public virtual IEnumerable<Class> ApprovedClasses { get; set; }
        public virtual IEnumerable<ClassUpdateHistory> ClassUpdateHistories { get; set; }
        public virtual IEnumerable<ClassTrainee> ClassTrainees { get; set; }
        public virtual IEnumerable<ClassAdmin> ClassAdmins { get; set; }

        public virtual IEnumerable<ClassMentor> ClassMentors { get; set; }

        public class UpLoadExcelFileRequest
        {
            public IFormFile File { get; set; }
        }
        public class UpLoadExcelFileResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }


    }
}