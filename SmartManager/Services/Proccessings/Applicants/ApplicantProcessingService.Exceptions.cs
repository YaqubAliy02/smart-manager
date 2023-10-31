﻿//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Applicants.Exceptions;
using SmartManager.Models.Applicants;
using System.Threading.Tasks;
using Xeptions;
using EFxceptions.Models.Exceptions;
using System.Linq;
using System;
using System.Data.SqlClient;

namespace SmartManager.Services.Proccessings.Applicants
{
    public partial class ApplicantProcessingService
    {
        private delegate ValueTask<Applicant> ReturningApplicantFunction();
        private delegate IQueryable<Applicant> ReturningApplicantsFunction();

        private async ValueTask<Applicant> TryCatch(ReturningApplicantFunction returningApplicantFunction)
        {
            try
            {
                return await returningApplicantFunction();
            }
            catch (NullApplicantException nullApplicantExeption)
            {
                throw CreateAndLogValidationException(nullApplicantExeption);
            }
            catch (InvalidApplicantException invalidApplicantException)
            {
                throw CreateAndLogValidationException(invalidApplicantException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsApplicantException =
                    new AlreadyExistsApplicantException(duplicateKeyException);

                throw CreateAndALogDependencyValidationException(alreadyExistsApplicantException);
            }
            catch (SqlException sqlException)
            {
                var failedApplicantStorageException = new FailedApplicantStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedApplicantStorageException);
            }
            catch (Exception exception)
            {
                var failedApplicantServiceException = new FailedApplicantServiceException(exception);

                throw CreateAndLogServiceException(failedApplicantServiceException);
            }
        }

        private IQueryable<Applicant> TryCatch(ReturningApplicantsFunction returningApplicantsFunction)
        {
            try
            {
                return returningApplicantsFunction();
            }
            catch (SqlException SqlException)
            {
                var failedApplicantStorageException =
                    new FailedApplicantStorageException(SqlException);

                throw CreateAndLogCriticalDependencyException(failedApplicantStorageException);
            }
            catch (Exception exception)
            {
                var failedApplicantServiceException =
                    new FailedApplicantServiceException(exception);

                throw CreateAndLogServiceException(failedApplicantServiceException);
            }
        }

        private ApplicantDependencyValidationException CreateAndALogDependencyValidationException(Xeption exception)
        {
            var applicantDependencyValidationException =
                new ApplicantDependencyValidationException(exception);
            this.loggingBroker.LogError(applicantDependencyValidationException);

            return applicantDependencyValidationException;
        }

        private ApplicantDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var applicantDependencyException = new ApplicantDependencyException(exception);
            this.loggingBroker.LogCritical(applicantDependencyException);

            return applicantDependencyException;
        }

        private ApplicantServiceException CreateAndLogServiceException(Xeption exception)
        {
            var applicantServiceException = new ApplicantServiceException(exception);
            this.loggingBroker.LogError(applicantServiceException);

            return applicantServiceException;
        }

        private ApplicantProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var applicantProcessingValidationException =
                new ApplicantProcessingValidationException(exception);

            this.loggingBroker.LogError(applicantProcessingValidationException);

            return applicantProcessingValidationException;
        }
    }
}
