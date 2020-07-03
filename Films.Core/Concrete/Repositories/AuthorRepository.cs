﻿using System.Collections.Generic;
 using System.Threading.Tasks;
 using Films.Core.Abstract.Repositories;
using Films.Core.Concrete.Models;
 using Microsoft.EntityFrameworkCore;

namespace Films.Core.Concrete.Repositories
{
    public class AuthorRepository: Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<Author>> GetAuthorsWithVideos()
        {
            return await Context.Set<Author>().Include(a => a.Videos).ToListAsync();
        }

        public async Task<Author> GetAuthorWithVideos(int id)
        {
            return await Context.Set<Author>().Include(a => a.Videos)
                .SingleOrDefaultAsync(a => a.Id == id);
        }
    }
}