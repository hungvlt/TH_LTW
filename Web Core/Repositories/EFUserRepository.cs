//using Microsoft.EntityFrameworkCore;
//using Web_Core.Models;

//namespace Web_Core.Repositories
//{
//   public class EFUserRepository : IUserRepository
//   {
//      private readonly ApplicationDbContext _context;

//      public EFUserRepository(ApplicationDbContext context)
//      {
//         _context = context;
//      }

//      public async Task<IEnumerable<User>> GetAllAsync()
//      {
//         return await _context.Users.ToListAsync();
//      }

//      public async Task<User> GetByIdAsync(int id)
//      {
//         return await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
//      }

//      public async Task AddAsync(User user)
//      {
//         _context.Users.Add(user);
//         await _context.SaveChangesAsync();
//      }

//      public async Task UpdateAsync(User user)
//      {
//         _context.Users.Update(user);
//         await _context.SaveChangesAsync();
//      }

//      public async Task DeleteAsync(int id)
//      {
//         var user = await _context.Users.FindAsync(id);
//         if (user != null)
//         {
//            _context.Users.Remove(user);
//            await _context.SaveChangesAsync();
//         }
//      }
//   }
//}
