using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.EFLikeDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFLike
{
	public class EFLikeService : IEFLikeService
	{
		private readonly FGShopContext _context;

		public EFLikeService(FGShopContext context)
		{
			_context = context;
		}

		public async Task<bool> CheckLikeStatusAsync(int productId, int userId)
		{
			var data = await _context.Likes
				.Where(x => x.ProductId == productId && x.UserId == userId)
				.FirstOrDefaultAsync();
			if (data == null)
			{
				return false;
			}
			return true;
		}

		public async Task<List<ResultLikeDto>> GetAll(int userId)
		{
			var list = await _context.Likes
				.Where(x => x.UserId == userId)
				.Select(like => new ResultLikeDto
				{
					LikeId = like.Id,
					ProductId = like.Product.Id,
					ProductName = like.Product.ProductName,
					Price = like.Product.Price,
					UserId = userId,
					ConvertPhoto = like.Product.CoverPhoto

				})
				.ToListAsync();

			return list;
		}

		public async Task<int> GetByProductIdandUserId(int productId, int userId)
		{
			var data = await _context.Likes
				.Where(x => x.ProductId == productId && x.UserId == userId)
				.FirstOrDefaultAsync();
			return data.Id;
		}

        public async Task<int> GetByUserIdLikeQuantity(int userId)
        {
            var data = await _context.Likes
				.Where (x => x.UserId == userId)
				.CountAsync();

			return data;
        }

        public async Task<bool> RemoveLike(int productId, int userId)
		{
			// İlgili kaydı bul
			var like = await _context.Likes
				.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);

			// Eğer kayıt bulunamazsa, işlem başarısız olarak döndür
			if (like == null)
			{
				return false; // Silinecek bir kayıt yok
			}

			// Kayıt bulundu, silme işlemini yap
			_context.Likes.Remove(like);

			// Değişiklikleri veritabanına kaydet ve başarılı durumunu döndür
			await _context.SaveChangesAsync();
			return true; // Eğer değişiklik yapıldıysa true döner
		}

	}
}
