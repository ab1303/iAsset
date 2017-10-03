using AutoMapper;

namespace iAsset.WebApi.Mapping
{
    public static class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfiguration;

        public static IMapper CreateMapper()
        {
            return _mapperConfiguration.CreateMapper();
        }

        public static void Configure()
        {
            _mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<SecureCoProfile>();
            });
            _mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}