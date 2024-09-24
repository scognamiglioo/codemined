using learncode.Data;
using learncode.Models;

public class StateContainer
{
    public List<User> Users { get; set; } = new List<User>();
    public User EditUser { get; set; }
    public int CurrentUserId { get; set; }
    
    public event Action OnChange;
    
    public void NotifyStateChanged() => OnChange?.Invoke();
}
