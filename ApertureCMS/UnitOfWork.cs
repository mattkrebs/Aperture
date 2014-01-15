using ApertureCMS.Models;
using ApertureCMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureCMS
{
    public class UnitOfWork : IDisposable
    {
        private ApertureDataContext context = new ApertureDataContext();
        private ApertureRepository<Photo> photoRepository;
        private ApertureRepository<Gallery> galleryRepository;
        private ApertureRepository<BlogEntry> blogRepository;
        private ApertureRepository<Page> pageRepository;

        public ApertureRepository<Page> PageRepository
        {
            get
            {

                if (this.pageRepository == null)
                {
                    this.pageRepository = new ApertureRepository<Page>(context);
                }
                return pageRepository;
            }
        }

        public ApertureRepository<BlogEntry> BlogRepository
        {
            get
            {

                if (this.blogRepository == null)
                {
                    this.blogRepository = new ApertureRepository<BlogEntry>(context);
                }
                return blogRepository;
            }
        }
        public ApertureRepository<Photo> PhotoRepostitory
        {
            get
            {

                if (this.photoRepository == null)
                {
                    this.photoRepository = new ApertureRepository<Photo>(context);
                }
                return photoRepository;
            }
        }
        public ApertureRepository<Gallery> GalleryRepostitory
        {
            get
            {

                if (this.galleryRepository == null)
                {
                    this.galleryRepository = new ApertureRepository<Gallery>(context);
                }
                return galleryRepository;
            }
        }
       

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
