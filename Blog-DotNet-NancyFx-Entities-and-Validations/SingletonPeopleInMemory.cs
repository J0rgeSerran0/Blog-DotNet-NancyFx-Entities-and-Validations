namespace Blog_DotNet_NancyFx_Entities_and_Validations
{

    using System;

    public class SingletonPeopleInMemory
    {
        private static volatile PeopleInMemory instance;
        private static Object syncRootObject = new Object();

        public static PeopleInMemory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRootObject)
                    {
                        if (instance == null)
                        {
                            instance = new PeopleInMemory();
                        }
                    }
                }

                return instance;
            }
        }
    }

}