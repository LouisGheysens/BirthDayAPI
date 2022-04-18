using AutoMapper;
using Business.Services;
using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    public class BirthdayBaseService : IDisposable
    {
        public BirthDayDatabaseContext Context;
        public IMapper Mapper;



        public BirthdayBaseService(BirthDayDatabaseContext context, IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Create the Exception message for try catch blocks
        /// </summary>
        /// <param name="methodName">The name of the current method</param>
        /// <param name="ex">the Exception</param>
        /// <param name="lineNumber"></param>
        public void SetErrorMsg(string methodName, Exception ex, int lineNumber)
        {
            var serviceResponse = new ServiceResponse<object>
            {
                Success = false,
                Message = $"Er is een fout gebeurd, probeer opnieuw aub.",
                Data = $"{methodName}_Line:{lineNumber} {Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}"
            };
        }



        public async Task<bool> Save(CancellationToken token)
        {
            return await Context.SaveChangesAsync(token) >= 0;
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Context == null) return;
            Context.Dispose();
            Context = null;
        }
    }
}
