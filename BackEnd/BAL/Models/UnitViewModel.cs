namespace BAL.Models
{
    public class UnitViewModel
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        //public long? IdSession { get; set; }
        //team 01
        public int Index { get; set; }
        public int? Status { get; set; }
        //team 01
        public List<LessonViewModel>? Lessons { get; set; }
    }
}
