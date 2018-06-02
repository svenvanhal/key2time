namespace Timetabling.Algorithms.FET
{

#pragma warning disable 1591
    public sealed class FetLanguage
    {
        public static readonly FetLanguage Arabic = new FetLanguage("ar");
        public static readonly FetLanguage Catalan = new FetLanguage("ca");
        public static readonly FetLanguage Czech = new FetLanguage("cs");
        public static readonly FetLanguage Danish = new FetLanguage("da");
        public static readonly FetLanguage German = new FetLanguage("de");
        public static readonly FetLanguage Greek = new FetLanguage("el");
        public static readonly FetLanguage British_English = new FetLanguage("en_GB");
        public static readonly FetLanguage US_English = new FetLanguage("en_US");
        public static readonly FetLanguage Spanish = new FetLanguage("es");
        public static readonly FetLanguage Basque = new FetLanguage("eu");
        public static readonly FetLanguage Persian = new FetLanguage("fa");
        public static readonly FetLanguage French = new FetLanguage("fr");
        public static readonly FetLanguage Galician = new FetLanguage("gl");
        public static readonly FetLanguage Hebrew = new FetLanguage("he");
        public static readonly FetLanguage Hungarian = new FetLanguage("hu");
        public static readonly FetLanguage Indonesian = new FetLanguage("id");
        public static readonly FetLanguage Italian = new FetLanguage("it");
        public static readonly FetLanguage Japanese = new FetLanguage("ja");
        public static readonly FetLanguage Lithuanian = new FetLanguage("lt");
        public static readonly FetLanguage Macedonian = new FetLanguage("mk");
        public static readonly FetLanguage Malay = new FetLanguage("ms");
        public static readonly FetLanguage Dutch = new FetLanguage("nl");
        public static readonly FetLanguage Polish = new FetLanguage("pl");
        public static readonly FetLanguage Brazilian_Portuguese = new FetLanguage("pt_BR");
        public static readonly FetLanguage Romanian = new FetLanguage("ro");
        public static readonly FetLanguage Russian = new FetLanguage("ru");
        public static readonly FetLanguage Sinhala = new FetLanguage("si");
        public static readonly FetLanguage Slovak = new FetLanguage("sk");
        public static readonly FetLanguage Albanian = new FetLanguage("sq");
        public static readonly FetLanguage Serbian = new FetLanguage("sr");
        public static readonly FetLanguage Turkish = new FetLanguage("tr");
        public static readonly FetLanguage Ukranian = new FetLanguage("uk");
        public static readonly FetLanguage Uzbek = new FetLanguage("uz");
        public static readonly FetLanguage Vietnamese = new FetLanguage("vi");
        public static readonly FetLanguage Simplified_Chinese = new FetLanguage("zh_CN");
        public static readonly FetLanguage Traditional_Chinese = new FetLanguage("zh_TW");

        private readonly string _languageName;

        private FetLanguage(string languageName) => _languageName = languageName;

        /// <inheritdoc />
        public override string ToString() => _languageName;
    }
#pragma warning restore 1591

}
