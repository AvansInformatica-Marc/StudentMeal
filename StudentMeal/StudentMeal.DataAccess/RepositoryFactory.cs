using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMeal.DataAccess {
    public class RepositoryFactory {
        private IServiceProvider _serviceProvider;
        public RepositoryFactory(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IRepository DatabaseRepository {
            get {
                return new DatabaseRepository(_serviceProvider.GetRequiredService<StudentMealDbContext>());
            }
        }

        public IRepository FakeDataRepository {
            get {
                return new FakeDataRepository();
            }
        }
    }
}
