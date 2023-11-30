namespace WembleyScada.Api.Application.Queries.ShiftReports;
public class ShiftReportViewModel
{
    public int Id { get; set; }
    public double OEE { get; set; }
    public double A { get; set; }
    public double P { get; set; }
    public double Q { get; set; }
    public DateTime Date { get; set; }
    public int ShiftNumber { get; set; }
    public string DeviceId { get; set; }
    public int ProductCount { get; set; }
    public int DefectCount { get; set; }

    public ShiftReportViewModel(int id, double oEE, double a, double p, double q, DateTime date, int shiftNumber, string deviceId, int productCount, int defectCount)
    {
        Id = id;
        OEE = oEE;
        A = a;
        P = p;
        Q = q;
        Date = date;
        ShiftNumber = shiftNumber;
        DeviceId = deviceId;
        ProductCount = productCount;
        DefectCount = defectCount;
    }
}
