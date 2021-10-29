﻿using MBKM.Common.Interfaces;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Services.MBKMServices
{
    public interface INilaiKuliahService : IEntityService<NilaiKuliah>
    {
        VMBobot GetBobot(string idMatkul);
        IEnumerable<VMSubBobot> GetSubBobot(string idMatkul);
    }
    public class NilaiKuliahService : EntityService<NilaiKuliah>, INilaiKuliahService
    {
        IUnitOfWork _unitOfWork;
        INilaiKuliahRepository _nilaiKuliahRepository;

        public NilaiKuliahService(IUnitOfWork unitOfWork, INilaiKuliahRepository NilaiKuliahRepository)
            : base(unitOfWork, NilaiKuliahRepository)
        {
            _unitOfWork = unitOfWork;
            _nilaiKuliahRepository = NilaiKuliahRepository;
        }
        public VMBobot GetBobot(string idMatkul)
        {
            return _nilaiKuliahRepository.GetBobot(idMatkul);
        }
        public IEnumerable<VMSubBobot> GetSubBobot(string idMatkul)
        {
            return _nilaiKuliahRepository.GetSubBobot(idMatkul);
        }
    }
}
