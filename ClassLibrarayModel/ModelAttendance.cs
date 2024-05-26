namespace ClassLibraryModel
{
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public AttendanceStatus Status { get; set; }
    }

    public enum AttendanceStatus
    {
        Present,
        Absent,
        Late
    }
}
