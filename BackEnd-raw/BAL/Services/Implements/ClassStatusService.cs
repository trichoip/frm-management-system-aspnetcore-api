﻿using BAL.Services.Interfaces;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class ClassStatusService: IClassStatusService
    {
        private IClassStatusRepository _classStatusRepository;
        private IUnitOfWork _unitOfWork;

        public ClassStatusService(IClassStatusRepository classStatusRepository, IUnitOfWork unitOfWork)
        {
            _classStatusRepository = classStatusRepository;
            _unitOfWork = unitOfWork;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }
    }
}
