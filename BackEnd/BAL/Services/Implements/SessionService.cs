using AutoMapper;
using BAL.AutoMapperProfile;
using BAL.Models;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class SessionService : ISessionService
    {
        private ISessionRepository _sessionRepository;
        private IUnitOfWork _unitOfWork;
        // Team6
        private readonly IMapper _mapperSession;

        public SessionService(ISessionRepository sessionRepository, IUnitOfWork unitOfWork, IMapper mapperSession)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _mapperSession = mapperSession;
        }


        public List<SessionViewModel> GetSessions(long id)
        {
            List<SessionViewModel> viewModel = null;
            viewModel = _mapperSession.Map<List<SessionViewModel>>(_sessionRepository.GetSessions(id));
            return viewModel;
        }
        // Team6

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
