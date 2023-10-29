﻿//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Applicants;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SmartManager.Services.Proccessings.Applicants
{
    public interface IApplicantProcessingService
    {
        ValueTask<Applicant> AddApplicantAsync(Applicant applicant);
        ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid applicantid);
        IQueryable<Applicant> RetrieveAllApplicants();
        ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant);
        ValueTask<Applicant> RemoveApplicantAsync(Guid applicantid);
    }
}
