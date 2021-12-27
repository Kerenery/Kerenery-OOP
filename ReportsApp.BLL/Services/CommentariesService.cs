using System;
using System.Collections.Generic;
using ReportsApp.BLL.Interfaces;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Repositories;

namespace ReportsApp.BLL.Services
{
    public class CommentariesService : ICommentariesService
    {
        private readonly CommentariesRepository _repository;

        public CommentariesService(CommentariesRepository repository)
        {
            _repository = repository;
        }

        public void CreateCommentary(Commentary commentary)
        {
            _repository.AddCommentary(commentary);
        }   

        public void DeleteCommentary(Guid id)
        {
            _repository.DeleteCommentary(id);
        }

        public List<Commentary> GetAll()
        {
            return _repository.GetAll();
        }

        public Commentary GetById(Guid id)
        {
            return _repository.GetCommentaryById(id);
        }

        public List<Commentary> GetCommentsByUser(Guid id)
        {
            return _repository.GetCommentsByUser(id);
        }

        public List<Commentary> GetCommentsByTask(Guid id)
        {
            return _repository.GetCommentsByTask(id);
        }
    }
}