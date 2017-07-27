namespace Blog_DotNet_NancyFx_Entities_and_Validations.Modules
{

    using Blog_DotNet_NancyFx_Entities_and_Validations.Entities;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Validation;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PersonModule : NancyModule
    {
        public PersonModule()
        {
            Get[@"/person/list", runAsync: true] = async (_, cancellationToken) =>
            {
                List<PersonEntity> people = await GetPeopleAsync();
                return Response.AsJson<List<PersonEntity>>(people);
            };

            Post["/person", runAsync: true] = async (_, cancellationToken) =>
            {
                var personEntity = this.Bind<PersonEntity>();
                var validationResult = this.Validate(personEntity);
                if (!validationResult.IsValid)
                {
                    var errors = String.Empty;
                    foreach (var error in validationResult.Errors)
                    {
                        errors += error.Value[0].ErrorMessage + Environment.NewLine;
                    }

                    var response = (Response)errors;
                    response.StatusCode = HttpStatusCode.BadRequest;

                    return response;
                }

                return await PostPeopleAsync(personEntity);
            };
        }

        private async Task<List<PersonEntity>> GetPeopleAsync()
        {
            return await Task.FromResult(SingletonPeopleInMemory.Instance.GetAll());
        }

        private async Task<HttpStatusCode> PostPeopleAsync(PersonEntity personEntity)
        {
            try
            {
                SingletonPeopleInMemory.Instance.Add(personEntity);

                return await Task.FromResult(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return await Task.FromResult(HttpStatusCode.InternalServerError);
            }
        }

    }

}