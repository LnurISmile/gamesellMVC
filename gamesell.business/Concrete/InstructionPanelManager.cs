using gamesell.business.Abstract;
using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Concrete
{
    public class InstructionPanelManager : IInstructionPanelService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InstructionPanelManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(InstructionPanel entity)
        {
            _unitOfWork.IPs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(InstructionPanel entity)
        {
            _unitOfWork.IPs.Delete(entity);
            _unitOfWork.Save();
        }

        public List<InstructionPanel> GetAll(int page, int pageSize)
        {
            return _unitOfWork.IPs.GetAll(page, pageSize);
        }

        public List<InstructionPanel> GetAll()
        {
            return _unitOfWork.IPs.GetAll();
        }

        public InstructionPanel GetById(int id)
        {
            return _unitOfWork.IPs.GetById(id);
        }

        public void Update(InstructionPanel entity)
        {
            _unitOfWork.IPs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
