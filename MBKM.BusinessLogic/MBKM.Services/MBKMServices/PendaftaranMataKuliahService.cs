﻿using MBKM.Common.Helpers;
using MBKM.Common.Interfaces;
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
    public interface IPendaftaranMataKuliahService : IEntityService<PendaftaranMataKuliah>
    {
        IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search);
        IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search);
        IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string idProdi, string search);

        VMListPendaftaranMataKuliah GetPendaftaranMahasiswaDataTable(DataTableAjaxPostModel model);
        VMListPendaftaranMataKuliah GetPendaftaranMahasiswaDataTableByMahasiswa(DataTableAjaxPostModel model, string emailMahasiswa);
    }
    public class PendaftaranMataKuliahService : EntityService<PendaftaranMataKuliah>, IPendaftaranMataKuliahService
    {
        IUnitOfWork _unitOfWork;
        IPendaftaranMataKuliahRepository _pmkRepository;

        public PendaftaranMataKuliahService(IUnitOfWork unitOfWork, IPendaftaranMataKuliahRepository PMKRepository)
            : base(unitOfWork, PMKRepository)
        {
            _unitOfWork = unitOfWork;
            _pmkRepository = PMKRepository;
        }

        public IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search)
        {
            return _pmkRepository.GetFakultas(jenjangStudi, search);
        }
        public IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search)
        {
            return _pmkRepository.GetProdiByFakultas(jenjangStudi, idFakultas, search);
        }
        public IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string idProdi, string search)
        {
            return _pmkRepository.GetLokasiByProdi(jenjangStudi, idProdi, search);
        }

        public VMListPendaftaranMataKuliah GetPendaftaranMahasiswaDataTable(DataTableAjaxPostModel model)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();
            return _pmkRepository.GetPendaftaranList(skip, take, searchBy, sortBy, sortDir);
        }

        public VMListPendaftaranMataKuliah GetPendaftaranMahasiswaDataTableByMahasiswa(DataTableAjaxPostModel model, string emailMahasiswa)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();
            return _pmkRepository.GetPendaftaranListFromMahasiswa(skip, take, searchBy, sortBy, sortDir, emailMahasiswa);
        }
    }
}
