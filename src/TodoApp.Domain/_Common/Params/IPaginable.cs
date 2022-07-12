namespace TodoApp.Domain._Common.Params
{
    public interface IPaginable : IParams
    {
        int? Skip { get; set; }
        int? Take { get; set; }
    }
}
