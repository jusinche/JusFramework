namespace JusNucleo.Bl.Comun
{
    public class AplicacionVersion
    {
        private static AplicacionVersion _appVersion;
        private AplicacionVersion()
        {
        }

        public string Version
        {
            get { return typeof(AplicacionVersion).Assembly.GetName().Version.ToString(); }
        }

        public static AplicacionVersion Get()
        {
            if (_appVersion==null)
            {
                _appVersion= new AplicacionVersion();
            }
            return _appVersion;
        }
    }
}
