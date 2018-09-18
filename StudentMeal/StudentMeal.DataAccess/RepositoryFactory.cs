using System;
using System.Collections.Generic;
using System.Text;

namespace StudentMeal.DataAccess {
    public class RepositoryFactory {
        public static IRepository DatabaseRepository {
            get {
                return new DatabaseRepository();
            }
        }

        public static IRepository FakeDataRepository {
            get {
                return new FakeDataRepository();
            }
        }
    }
}
