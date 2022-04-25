using AutoMapper;
using Business.Azure;
using Business.Base;
using Business.Interfaces;
using Data;
using Data.Models;
using Dto.Models;
using Gridify;
using Gridify.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class PersonService : BirthdayBaseService, IPersonService
    {
        public PersonService(BirthDayDatabaseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ServiceResponse<List<PersonDto>>> GetPersonsAsync(GridifyQuery gQuery, CancellationToken token)
        {
            var serviceResponse = new ServiceResponse<List<PersonDto>>();

            var (count, query) = await Context.Persons
                .OrderBy(x => x.FirstName)
                .Where(x => !x.Deleted)
                .Include(x => x.Address)
                .GridifyQueryableAsync(gQuery, new GridifyMapper<Person>().GenerateMappings()
                .AddMap("name", q => q.FirstName)
                .AddMap("address", q => q.Address.City)
                , token);

            var dataObject = Mapper.Map<List<PersonDto>>(query);


            if (!dataObject.Any())
            {
                serviceResponse.Success = false;
                serviceResponse.Data = new List<PersonDto>();
                serviceResponse.Message = "NotFound.Error";
                return serviceResponse;
            }

            serviceResponse.TotalRecords = count;
            serviceResponse.Success = true;
            serviceResponse.Data = dataObject;
            return serviceResponse;
        }

        public async Task<ServiceResponse<PersonDto>> GetPerson(int id, CancellationToken token)
        {
            var serviceResponse = new ServiceResponse<PersonDto>();
            var person = await Context.Persons.FirstOrDefaultAsync(x => x.Id == id, token);

            if (person is null)
            {
                serviceResponse.Success = false;
                return serviceResponse;
            }
            var dataObject = Mapper.Map<PersonDto>(person);
            
            if(person.LogoFileName is not null)
            {
                var obj = AzureSingleton.Instance.GetFileFromAzure(person.LogoFileName);
                dataObject.LogoFileName = obj;
            }

            serviceResponse.Data = dataObject;
            return serviceResponse;
        }


        public async Task<ServiceResponse<int>> AddPerson(PersonDto dto, CancellationToken token)
        {

            var serviceResponse = new ServiceResponse<int>();
            var mappedPerson = Mapper.Map<Person>(dto);

            Context.Persons.Add(mappedPerson);
            AzureSingleton.Instance.UploadToAzure(mappedPerson.LogoFileName);

            mappedPerson.CreationDate = DateTime.Now;


            await Save(token);

            if (!await Save(token))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "CreatePerson.Error";
                return serviceResponse;
            }

            serviceResponse.Success = true;
            serviceResponse.Data = mappedPerson.Id;
            return serviceResponse;
        }



        public async Task<ServiceResponse<int>> UpdatePerson(int id, PersonDto dto, CancellationToken token)
        {
            var serviceResponse = new ServiceResponse<int>();
            var oldPerson = await Context.Persons.FirstOrDefaultAsync(x => x.Id == id, token);

            if (oldPerson == null)
            {
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var mappedPerson = Mapper.Map<Person>(dto);

            mappedPerson.ModificationDate = DateTime.Now;


            Context.Entry(oldPerson).CurrentValues.SetValues(mappedPerson);

            if(oldPerson.LogoFileName != mappedPerson.LogoFileName)
            {
                //Delete old blob file
                AzureSingleton.Instance.DeleteFromAzure(oldPerson.LogoFileName);

                //Add new blob file
                AzureSingleton.Instance.UploadToAzure(mappedPerson.LogoFileName);
            }

            await Save(token);

            if (!await Save(token))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "UpdatePerson.Error";
                return serviceResponse;
            }

            serviceResponse.Success = true;
            serviceResponse.Data = mappedPerson.Id;
            return serviceResponse;
        }

        public async Task<ServiceResponse<object>> DeletePerson(int id, CancellationToken token)
        {
            var serviceResponse = new ServiceResponse<object>();
            var oldClub = await Context.Persons.FirstOrDefaultAsync(x => x.Id == id, token);

            if(oldClub.LogoFileName != null) { AzureSingleton.Instance.DeleteFromAzure(oldClub.LogoFileName); }
            if (oldClub != null) oldClub.Deleted = true;


            if (!await Save(token))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "DeletePerson.Error";
                return serviceResponse;
            }

            serviceResponse.Success = true;
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> UploadImage(int id, string file, CancellationToken token)
        {
            var serviceResponse = new ServiceResponse<bool>();
            var person = await Context.Persons.FirstOrDefaultAsync(x => x.Id == id, token);

            if (person is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "PersonNotFound.Error";
                return serviceResponse;
            }

            person.ModificationDate = DateTime.Now;

            var correctFile = AzureSingleton.Instance.UploadToAzure(file);

            person.LogoFileName = correctFile;

            if (!await Save(token))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "LogoUpload.Error";
                return serviceResponse;
            }

            serviceResponse.Success = true;
            return serviceResponse;
        }
    }
}
