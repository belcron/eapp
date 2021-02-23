using DataAccess.Generic;
using Entities.Domain;
using NUnit.Framework;

namespace TestProject
{
    public class Tests
    {
        private  IGenericRepository<Client> _genericRepository;
        private  IUnitOfWork _unitOfWork;



        public Tests()
        {
            genericRepository = new IGenericRepository<Client>();
            unitOfWork = new IUnitOfWork();
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        [SetUp]
        public void Setup()
        {
          
        }


        [Test]
        public void Test1()
        {
            var myObj = new Client();
            {
                myObj.LastName = "Lopez";
                myObj.FirstName = "Jose";
                myObj.Email = "lp@gmail.com";

                bool created = false;

                try
                {
                    var save = await _unitOfWork.Context.Set<T>().AddAsync(entity);

                    if (save != null)
                        created = true;
                }
                catch (Exception)
                {
                    throw;
                }
                return created;
            }
            Assert.Pass();
        }
    }
}