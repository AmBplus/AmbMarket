namespace ambMarket.Infrastructure.Utilities;

public class ErrorType
{
    /// <summary>
    /// درخواست مشکلی دارد
    /// </summary>
    public static string _400 { get => "درخواست کاربر مشکلی دارد"; }
    /// <summary>
    /// مشکلی در احراز هویت داریم
    /// </summary>
    public static string _401 { get => "مشکلی در احراز هویت داریم"; }
    /// <summary>
    /// صفحه مورد نظر پیدا نشد
    /// </summary>
    public static string _404 { get => "صفحه مورد نظر پیدا نشد"; }
    /// <summary>
    /// مشکلی پیش آمده صبور باشید
    /// </summary>
    public static string _500 { get => "مشکلی پیش آمده صبور باشید"; }
}