using Microsoft.EntityFrameworkCore;
using vsl_crm_api.Models.Domains;

namespace vsl_crm_api.Data
{
    public partial class VslDbContext: DbContext
    {
        public VslDbContext(DbContextOptions<VslDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TblBaoCaoCongViec> TblBaoCaoCongViecs { get; set; }

        public virtual DbSet<TblCustomerListImEx> TblCustomerListImExes { get; set; }

        public virtual DbSet<TblCustomerTacNghiep> TblCustomerTacNghieps { get; set; }

        public virtual DbSet<TblDmaction> TblDmactions { get; set; }

        public virtual DbSet<TblDmbank> TblDmbanks { get; set; }

        public virtual DbSet<TblDmbankAccount> TblDmbankAccounts { get; set; }

        public virtual DbSet<TblDmchucVu> TblDmchucVus { get; set; }

        public virtual DbSet<TblDmcity> TblDmcities { get; set; }

        public virtual DbSet<TblDmcontactFun> TblDmcontactFuns { get; set; }

        public virtual DbSet<TblDmcontactList> TblDmcontactLists { get; set; }

        public virtual DbSet<TblDmcontractType> TblDmcontractTypes { get; set; }

        public virtual DbSet<TblDmcountry> TblDmcountries { get; set; }

        public virtual DbSet<TblDmcurrency> TblDmcurrencies { get; set; }

        public virtual DbSet<TblDmcustomer> TblDmcustomers { get; set; }

        public virtual DbSet<TblDmcustomerDanhGium> TblDmcustomerDanhGia { get; set; }

        public virtual DbSet<TblDmcustomerNghiepVu> TblDmcustomerNghiepVus { get; set; }

        public virtual DbSet<TblDmcustomerPhanLoaiKh> TblDmcustomerPhanLoaiKhs { get; set; }

        public virtual DbSet<TblDmcustomerTuyenHang> TblDmcustomerTuyenHangs { get; set; }

        public virtual DbSet<TblDmcustomerType> TblDmcustomerTypes { get; set; }

        public virtual DbSet<TblDmloaiDoanhNghiep> TblDmloaiDoanhNghieps { get; set; }

        public virtual DbSet<TblDmloaiHinhVanChuyen> TblDmloaiHinhVanChuyens { get; set; }

        public virtual DbSet<TblDmloaiNotification> TblDmloaiNotifications { get; set; }

        public virtual DbSet<TblDmloaiTacNghiep> TblDmloaiTacNghieps { get; set; }

        public virtual DbSet<TblDmnghiepVu> TblDmnghiepVus { get; set; }

        public virtual DbSet<TblDmphanLoaiKhachHang> TblDmphanLoaiKhachHangs { get; set; }

        public virtual DbSet<TblDmphongBan> TblDmphongBans { get; set; }

        public virtual DbSet<TblDmport> TblDmports { get; set; }

        public virtual DbSet<TblDmvanPhong> TblDmvanPhongs { get; set; }

        public virtual DbSet<TblDmxepLoaiBaoCaoCv> TblDmxepLoaiBaoCaoCvs { get; set; }

        public virtual DbSet<TblJobdocument> TblJobdocuments { get; set; }

        public virtual DbSet<TblJobnotification> TblJobnotifications { get; set; }

        public virtual DbSet<TblJobuserAccess> TblJobuserAccesses { get; set; }

        public virtual DbSet<TblJobuserPheDuyet> TblJobuserPheDuyets { get; set; }

        public virtual DbSet<TblNhanSu> TblNhanSus { get; set; }

        public virtual DbSet<TblNhanSuTreelist> TblNhanSuTreelists { get; set; }

        public virtual DbSet<TblNotification> TblNotifications { get; set; }

        public virtual DbSet<TblSysCode> TblSysCodes { get; set; }

        public virtual DbSet<TblSysCodeKey> TblSysCodeKeys { get; set; }

        public virtual DbSet<TblSysCodeKyHieu> TblSysCodeKyHieus { get; set; }

        public virtual DbSet<TblSysConfig> TblSysConfigs { get; set; }

        public virtual DbSet<TblSysFcmtoken> TblSysFcmtokens { get; set; }

        public virtual DbSet<TblSysOption> TblSysOptions { get; set; }

        public virtual DbSet<TblSysUser> TblSysUsers { get; set; }

        public virtual DbSet<TblSysUserGroup> TblSysUserGroups { get; set; }

        public virtual DbSet<TblSysUserNotification> TblSysUserNotifications { get; set; }

        public virtual DbSet<TblSysUserRelationGroup> TblSysUserRelationGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=dbdev.namanphu.vn;Initial Catalog=BestCare_DB;User Id=notification_user;Password=123456a$;Encrypt=false");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBaoCaoCongViec>(entity =>
            {
                entity.ToTable("tblBaoCaoCongViec");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.IdnhanVien).HasColumnName("IDNhanVien");
                entity.Property(e => e.IduserCreate).HasColumnName("IDUserCreate");
                entity.Property(e => e.IdxepLoai).HasColumnName("IDXepLoai");
                entity.Property(e => e.ThoiGianThucHien).HasColumnType("datetime");

                entity.HasOne(d => d.IdnhanVienNavigation).WithMany(p => p.TblBaoCaoCongViecs)
                    .HasForeignKey(d => d.IdnhanVien)
                    .HasConstraintName("FK_tblBaoCaoCongViec_tblNhanSu");

                entity.HasOne(d => d.IduserCreateNavigation).WithMany(p => p.TblBaoCaoCongViecs)
                    .HasForeignKey(d => d.IduserCreate)
                    .HasConstraintName("FK_tblBaoCaoCongViec_tblSysUser");

                entity.HasOne(d => d.IdxepLoaiNavigation).WithMany(p => p.TblBaoCaoCongViecs)
                    .HasForeignKey(d => d.IdxepLoai)
                    .HasConstraintName("FK_tblBaoCaoCongViec_tblDMXepLoaiBaoCaoCV");
            });

            modelBuilder.Entity<TblCustomerListImEx>(entity =>
            {
                entity.ToTable("tblCustomerListImEx");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Commd).HasColumnName("COMMD");
                entity.Property(e => e.CountryPod).HasColumnName("CountryPOD");
                entity.Property(e => e.CountryPol).HasColumnName("CountryPOL");
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.IdcustomerList).HasColumnName("IDCustomerList");
                entity.Property(e => e.Iddmcustomer).HasColumnName("IDDMCustomer");
                entity.Property(e => e.IddmdieuKienVc).HasColumnName("IDDMDieuKienVC");
                entity.Property(e => e.IdfromCountry).HasColumnName("IDFromCountry");
                entity.Property(e => e.IdfromPort).HasColumnName("IDFromPort");
                entity.Property(e => e.IdtoCountry).HasColumnName("IDToCountry");
                entity.Property(e => e.IdtoPort).HasColumnName("IDToPort");
                entity.Property(e => e.IduserCreate).HasColumnName("IDUserCreate");
                entity.Property(e => e.Pod).HasColumnName("POD");
                entity.Property(e => e.Pol).HasColumnName("POL");
                entity.Property(e => e.Unt).HasColumnName("UNT");

                entity.HasOne(d => d.IddmcustomerNavigation).WithMany(p => p.TblCustomerListImExes)
                    .HasForeignKey(d => d.Iddmcustomer)
                    .HasConstraintName("FK_tblCustomerListImEx_tblDMCustomer");

                entity.HasOne(d => d.IdfromCountryNavigation).WithMany(p => p.TblCustomerListImExIdfromCountryNavigations)
                    .HasForeignKey(d => d.IdfromCountry)
                    .HasConstraintName("FK_tblCustomerListImEx_tblDMCountry1");

                entity.HasOne(d => d.IdfromPortNavigation).WithMany(p => p.TblCustomerListImExIdfromPortNavigations)
                    .HasForeignKey(d => d.IdfromPort)
                    .HasConstraintName("FK_tblCustomerListImEx_tblDMPort1");

                entity.HasOne(d => d.IdtoCountryNavigation).WithMany(p => p.TblCustomerListImExIdtoCountryNavigations)
                    .HasForeignKey(d => d.IdtoCountry)
                    .HasConstraintName("FK_tblCustomerListImEx_tblDMCountry");

                entity.HasOne(d => d.IdtoPortNavigation).WithMany(p => p.TblCustomerListImExIdtoPortNavigations)
                    .HasForeignKey(d => d.IdtoPort)
                    .HasConstraintName("FK_tblCustomerListImEx_tblDMPort");

                entity.HasOne(d => d.IduserCreateNavigation).WithMany(p => p.TblCustomerListImExes)
                    .HasForeignKey(d => d.IduserCreate)
                    .HasConstraintName("FK_tblCustomerListImEx_tblSysUser");
            });

            modelBuilder.Entity<TblCustomerTacNghiep>(entity =>
            {
                entity.ToTable("tblCustomerTacNghiep");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DateCreate).HasColumnType("datetime");
                entity.Property(e => e.IdcustomerList).HasColumnName("IDCustomerList");
                entity.Property(e => e.Iddmcustomer).HasColumnName("IDDMCustomer");
                entity.Property(e => e.IdloaiTacNghiep).HasColumnName("IDLoaiTacNghiep");
                entity.Property(e => e.IdnguoiLienHe).HasColumnName("IDNguoiLienHe");
                entity.Property(e => e.IduserCreate).HasColumnName("IDUserCreate");
                entity.Property(e => e.NgayPhanHoi).HasColumnType("datetime");
                entity.Property(e => e.ThoiGianThucHien).HasColumnType("datetime");

                entity.HasOne(d => d.IddmcustomerNavigation).WithMany(p => p.TblCustomerTacNghieps)
                    .HasForeignKey(d => d.Iddmcustomer)
                    .HasConstraintName("FK_tblCustomerTacNghiep_tblDMCustomer");

                entity.HasOne(d => d.IdloaiTacNghiepNavigation).WithMany(p => p.TblCustomerTacNghieps)
                    .HasForeignKey(d => d.IdloaiTacNghiep)
                    .HasConstraintName("FK_tblCustomerTacNghiep_tblDMLoaiTacNghiep");

                entity.HasOne(d => d.IdnguoiLienHeNavigation).WithMany(p => p.TblCustomerTacNghieps)
                    .HasForeignKey(d => d.IdnguoiLienHe)
                    .HasConstraintName("FK_tblCustomerTacNghiep_tblDMContactList");

                entity.HasOne(d => d.IduserCreateNavigation).WithMany(p => p.TblCustomerTacNghieps)
                    .HasForeignKey(d => d.IduserCreate)
                    .HasConstraintName("FK_tblCustomerTacNghiep_tblSysUser");
            });

            modelBuilder.Entity<TblDmaction>(entity =>
            {
                entity.ToTable("tblDMAction");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Description).HasMaxLength(100);
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TblDmbank>(entity =>
            {
                entity.ToTable("tblDMBank");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Address).HasMaxLength(250);
                entity.Property(e => e.BankName).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.Fax).HasMaxLength(50);
                entity.Property(e => e.Logo).HasColumnType("image");
                entity.Property(e => e.Manager).HasMaxLength(50);
                entity.Property(e => e.Note).HasMaxLength(250);
                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<TblDmbankAccount>(entity =>
            {
                entity.ToTable("tblDMBankAccount");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.BankAccount).HasMaxLength(150);
                entity.Property(e => e.BankAddress).HasMaxLength(150);
                entity.Property(e => e.BankBranchName).HasMaxLength(150);
                entity.Property(e => e.BankName).HasMaxLength(150);
                entity.Property(e => e.BankOwner).HasMaxLength(150);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.FlagTkgiamDoc).HasColumnName("FlagTKGiamDoc");
                entity.Property(e => e.Idbank).HasColumnName("IDBank");
                entity.Property(e => e.Idcurrency).HasColumnName("IDCurrency");
                entity.Property(e => e.IdvanPhong).HasColumnName("IDVanPhong");

                entity.HasOne(d => d.IdbankNavigation).WithMany(p => p.TblDmbankAccounts)
                    .HasForeignKey(d => d.Idbank)
                    .HasConstraintName("FK_tblDMBankAccount_tblDMBank");

                entity.HasOne(d => d.IdcurrencyNavigation).WithMany(p => p.TblDmbankAccounts)
                    .HasForeignKey(d => d.Idcurrency)
                    .HasConstraintName("FK_tblDMBankAccount_tblDMCurrency");

                entity.HasOne(d => d.IdvanPhongNavigation).WithMany(p => p.TblDmbankAccounts)
                    .HasForeignKey(d => d.IdvanPhong)
                    .HasConstraintName("FK_tblDMBankAccount_tblDMVanPhong");
            });

            modelBuilder.Entity<TblDmchucVu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_EnumChucVu");

                entity.ToTable("tblDMChucVu");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.NameEN)
                    .HasMaxLength(250)
                    .HasColumnName("NameEN");
                entity.Property(e => e.NameVI)
                    .HasMaxLength(250)
                    .HasColumnName("NameVI");
            });

            modelBuilder.Entity<TblDmcity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblCity");

                entity.ToTable("tblDMCity");

                entity.HasIndex(e => e.IdquocGia, "iIDQuocGia_tblDMCity");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.IdquocGia).HasColumnName("IDQuocGia");
                entity.Property(e => e.NameEN)
                    .HasMaxLength(100)
                    .HasColumnName("NameEN");
                entity.Property(e => e.NameVI)
                    .HasMaxLength(100)
                    .HasColumnName("NameVI");
                entity.Property(e => e.Note)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.IdquocGiaNavigation).WithMany(p => p.TblDmcities)
                    .HasForeignKey(d => d.IdquocGia)
                    .HasConstraintName("FK_tblCity_tblQuocGia");
            });

            modelBuilder.Entity<TblDmcontactFun>(entity =>
            {
                entity.ToTable("tblDMContactFun");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FunctionalEn)
                    .HasMaxLength(150)
                    .HasColumnName("FunctionalEN");
                entity.Property(e => e.FunctionalVi)
                    .HasMaxLength(150)
                    .HasColumnName("FunctionalVI");
                entity.Property(e => e.Note).HasMaxLength(150);
            });

            modelBuilder.Entity<TblDmcontactList>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblDMNguoiLienHe");

                entity.ToTable("tblDMContactList");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AddressEN)
                    .HasMaxLength(250)
                    .HasColumnName("AddressEN");
                entity.Property(e => e.AddressVI)
                    .HasMaxLength(250)
                    .HasColumnName("AddressVI");
                entity.Property(e => e.BankAccountNumber).HasMaxLength(150);
                entity.Property(e => e.BankAddress).HasMaxLength(150);
                entity.Property(e => e.BankBranchName).HasMaxLength(150);
                entity.Property(e => e.Chat).HasMaxLength(250);
                entity.Property(e => e.ChucVu).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.HandPhone).HasMaxLength(50);
                entity.Property(e => e.HomePhone).HasMaxLength(50);
                entity.Property(e => e.Idbank).HasColumnName("IDBank");
                entity.Property(e => e.IdcontactFun).HasColumnName("IDContactFun");
                entity.Property(e => e.Idcustomer).HasColumnName("IDCustomer");
                entity.Property(e => e.Idport).HasColumnName("IDPort");
                entity.Property(e => e.NameEN)
                    .HasMaxLength(150)
                    .HasColumnName("NameEN");
                entity.Property(e => e.NameVI)
                    .HasMaxLength(150)
                    .HasColumnName("NameVI");
                entity.Property(e => e.Note).HasMaxLength(250);

                entity.HasOne(d => d.IdbankNavigation).WithMany(p => p.TblDmcontactLists)
                    .HasForeignKey(d => d.Idbank)
                    .HasConstraintName("FK_tblDMContactList_tblDMBank");

                entity.HasOne(d => d.IdcontactFunNavigation).WithMany(p => p.TblDmcontactLists)
                    .HasForeignKey(d => d.IdcontactFun)
                    .HasConstraintName("FK_tblDMNguoiLienHe_tblDMContactFun");

                entity.HasOne(d => d.IdcustomerNavigation).WithMany(p => p.TblDmcontactLists)
                    .HasForeignKey(d => d.Idcustomer)
                    .HasConstraintName("FK_tblDMContactList_tblDMCustomer");

                entity.HasOne(d => d.IdportNavigation).WithMany(p => p.TblDmcontactLists)
                    .HasForeignKey(d => d.Idport)
                    .HasConstraintName("FK_tblDMContactList_tblDMPort");
            });

            modelBuilder.Entity<TblDmcontractType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_EnumHinhThucHopDong");

                entity.ToTable("tblDMContractType");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.NameEN)
                    .HasMaxLength(50)
                    .HasColumnName("NameEN");
                entity.Property(e => e.NameVI)
                    .HasMaxLength(50)
                    .HasColumnName("NameVI");
                entity.Property(e => e.Note).HasMaxLength(250);
            });

            modelBuilder.Entity<TblDmcountry>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblQuocGia");

                entity.ToTable("tblDMCountry");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.NameEN)
                    .HasMaxLength(50)
                    .HasColumnName("NameEN");
                entity.Property(e => e.NameVI)
                    .HasMaxLength(50)
                    .HasColumnName("NameVI");
                entity.Property(e => e.Note).HasMaxLength(250);
            });

            modelBuilder.Entity<TblDmcurrency>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblEnumCurrency");

                entity.ToTable("tblDMCurrency");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.KyHieu).HasMaxLength(10);
                entity.Property(e => e.NameEN)
                    .HasMaxLength(50)
                    .HasColumnName("NameEN");
                entity.Property(e => e.NameVI)
                    .HasMaxLength(50)
                    .HasColumnName("NameVI");
                entity.Property(e => e.Note).HasMaxLength(250);
            });

            modelBuilder.Entity<TblDmcustomer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblDMKhachHang");

                entity.ToTable("tblDMCustomer");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AddressEN).HasColumnName("AddressEN");
                entity.Property(e => e.AddressVI).HasColumnName("AddressVI");
                entity.Property(e => e.BankAccountNumber).HasMaxLength(150);
                entity.Property(e => e.BankBranchName).HasMaxLength(150);
                entity.Property(e => e.Code).HasMaxLength(100);
                entity.Property(e => e.DateCreate).HasColumnType("datetime");
                entity.Property(e => e.DateDelete).HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Fax).HasMaxLength(150);
                entity.Property(e => e.FlagCrm).HasColumnName("FlagCRM");
                entity.Property(e => e.Idbank).HasColumnName("IDBank");
                entity.Property(e => e.Idcity).HasColumnName("IDCity");
                entity.Property(e => e.IdloaiDoanhNghiep).HasColumnName("IDLoaiDoanhNghiep");
                entity.Property(e => e.IdnhanVienSale).HasColumnName("IDNhanVienSale");
                entity.Property(e => e.IdquocGia).HasColumnName("IDQuocGia");
                entity.Property(e => e.IdtacNghiepCuoi).HasColumnName("IDTacNghiepCuoi");
                entity.Property(e => e.IduserCreate).HasColumnName("IDUserCreate");
                entity.Property(e => e.IduserDelete).HasColumnName("IDUserDelete");
                entity.Property(e => e.IduserGiaoViec).HasColumnName("IDUserGiaoViec");
                entity.Property(e => e.IduserTraKhach).HasColumnName("IDUserTraKhach");
                entity.Property(e => e.MaChiNhanh).HasMaxLength(100);
                entity.Property(e => e.MauTacNghiepCuoi).HasMaxLength(150);
                entity.Property(e => e.NameEN).HasColumnName("NameEN");
                entity.Property(e => e.NameVI).HasColumnName("NameVI");
                entity.Property(e => e.NgayChonKhach).HasColumnType("datetime");
                entity.Property(e => e.NgayChotKhach).HasColumnType("datetime");
                entity.Property(e => e.NgayGiao).HasColumnType("datetime");
                entity.Property(e => e.NgayKetThucNhan).HasColumnType("datetime");
                entity.Property(e => e.NgayNhan).HasColumnType("datetime");
                entity.Property(e => e.NgayTacNghiep).HasColumnType("datetime");
                entity.Property(e => e.NgayTraVe).HasColumnType("datetime");
                entity.Property(e => e.NgayTuTraKhach).HasColumnType("datetime");
                entity.Property(e => e.NgayTuongTac).HasColumnType("datetime");
                entity.Property(e => e.Phone).HasMaxLength(150);
                entity.Property(e => e.SttmaxTacNghiep).HasColumnName("STTMax_TacNghiep");
                entity.Property(e => e.TaxCode).HasMaxLength(30);
                entity.Property(e => e.Website).HasMaxLength(100);

                entity.HasOne(d => d.IdbankNavigation).WithMany(p => p.TblDmcustomers)
                    .HasForeignKey(d => d.Idbank)
                    .HasConstraintName("FK_tblDMCustomer_tblDMBank");

                entity.HasOne(d => d.IdcityNavigation).WithMany(p => p.TblDmcustomers)
                    .HasForeignKey(d => d.Idcity)
                    .HasConstraintName("FK_tblDMCustomer_tblDMCity");

                entity.HasOne(d => d.IdloaiDoanhNghiepNavigation).WithMany(p => p.TblDmcustomers)
                    .HasForeignKey(d => d.IdloaiDoanhNghiep)
                    .HasConstraintName("FK_tblDMCustomer_tblDMLoaiDoanhNghiep");

                entity.HasOne(d => d.IdnhanVienSaleNavigation).WithMany(p => p.TblDmcustomers)
                    .HasForeignKey(d => d.IdnhanVienSale)
                    .HasConstraintName("FK_tblDMCustomer_tblNhanSu");

                entity.HasOne(d => d.IdquocGiaNavigation).WithMany(p => p.TblDmcustomers)
                    .HasForeignKey(d => d.IdquocGia)
                    .HasConstraintName("FK_tblDMKhachHang_tblDMQuocGia");

                entity.HasOne(d => d.IduserCreateNavigation).WithMany(p => p.TblDmcustomerIduserCreateNavigations)
                    .HasForeignKey(d => d.IduserCreate)
                    .HasConstraintName("FK_tblDMCustomer_tblSysUser");

                entity.HasOne(d => d.IduserDeleteNavigation).WithMany(p => p.TblDmcustomerIduserDeleteNavigations)
                    .HasForeignKey(d => d.IduserDelete)
                    .HasConstraintName("FK_tblDMCustomer_tblSysUser1");

                entity.HasOne(d => d.IduserGiaoViecNavigation).WithMany(p => p.TblDmcustomerIduserGiaoViecNavigations)
                    .HasForeignKey(d => d.IduserGiaoViec)
                    .HasConstraintName("FK_tblDMCustomer_tblSysUser2");

                entity.HasOne(d => d.IduserTraKhachNavigation).WithMany(p => p.TblDmcustomerIduserTraKhachNavigations)
                    .HasForeignKey(d => d.IduserTraKhach)
                    .HasConstraintName("FK_tblDMCustomer_tblSysUser3");
            });

            modelBuilder.Entity<TblDmcustomerDanhGium>(entity =>
            {
                entity.ToTable("tblDMCustomerDanhGia");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DateCreate).HasColumnType("datetime");
                entity.Property(e => e.Iddmcustomer).HasColumnName("IDDMCustomer");
                entity.Property(e => e.IddmcustomerType).HasColumnName("IDDMCustomerType");
                entity.Property(e => e.IduserCreate).HasColumnName("IDUserCreate");

                entity.HasOne(d => d.IddmcustomerNavigation).WithMany(p => p.TblDmcustomerDanhGia)
                    .HasForeignKey(d => d.Iddmcustomer)
                    .HasConstraintName("FK_tblDMCustomerDanhGia_tblDMCustomer");

                entity.HasOne(d => d.IddmcustomerTypeNavigation).WithMany(p => p.TblDmcustomerDanhGia)
                    .HasForeignKey(d => d.IddmcustomerType)
                    .HasConstraintName("FK_tblDMCustomerDanhGia_tblDMCustomerType");

                entity.HasOne(d => d.IduserCreateNavigation).WithMany(p => p.TblDmcustomerDanhGia)
                    .HasForeignKey(d => d.IduserCreate)
                    .HasConstraintName("FK_tblDMCustomerDanhGia_tblSysUser");
            });

            modelBuilder.Entity<TblDmcustomerNghiepVu>(entity =>
            {
                entity.ToTable("tblDMCustomerNghiepVu");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Iddmcustomer).HasColumnName("IDDMCustomer");
                entity.Property(e => e.IddmnghiepVu).HasColumnName("IDDMNghiepVu");

                entity.HasOne(d => d.IddmcustomerNavigation).WithMany(p => p.TblDmcustomerNghiepVus)
                    .HasForeignKey(d => d.Iddmcustomer)
                    .HasConstraintName("FK_tblDMCustomerNghiepVu_tblDMCustomer");

                entity.HasOne(d => d.IddmnghiepVuNavigation).WithMany(p => p.TblDmcustomerNghiepVus)
                    .HasForeignKey(d => d.IddmnghiepVu)
                    .HasConstraintName("FK_tblDMCustomerNghiepVu_tblDMNghiepVu");
            });

            modelBuilder.Entity<TblDmcustomerPhanLoaiKh>(entity =>
            {
                entity.ToTable("tblDMCustomerPhanLoaiKH");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Iddmcustomer).HasColumnName("IDDMCustomer");
                entity.Property(e => e.IddmphanLoaiKhachHang).HasColumnName("IDDMPhanLoaiKhachHang");

                entity.HasOne(d => d.IddmcustomerNavigation).WithMany(p => p.TblDmcustomerPhanLoaiKhs)
                    .HasForeignKey(d => d.Iddmcustomer)
                    .HasConstraintName("FK_tblDMCustomerPhanLoaiKH_tblDMCustomer");

                entity.HasOne(d => d.IddmphanLoaiKhachHangNavigation).WithMany(p => p.TblDmcustomerPhanLoaiKhs)
                    .HasForeignKey(d => d.IddmphanLoaiKhachHang)
                    .HasConstraintName("FK_tblDMCustomerPhanLoaiKH_tblDMPhanLoaiKhachHang");
            });

            modelBuilder.Entity<TblDmcustomerTuyenHang>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Table_1_1");

                entity.ToTable("tblDMCustomerTuyenHang");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.EnumLoaiVanChuyen).HasColumnName("enumLoaiVanChuyen");
                entity.Property(e => e.IdcangDen).HasColumnName("IDCangDen");
                entity.Property(e => e.IdcangDi).HasColumnName("IDCangDi");
                entity.Property(e => e.Iddmcustomer).HasColumnName("IDDMCustomer");
                entity.Property(e => e.IddmloaiHinhVanChuyen).HasColumnName("IDDMLoaiHinhVanChuyen");
                entity.Property(e => e.IdquocGiaDen).HasColumnName("IDQuocGiaDen");
                entity.Property(e => e.IdquocGiaDi).HasColumnName("IDQuocGiaDi");

                entity.HasOne(d => d.IdcangDenNavigation).WithMany(p => p.TblDmcustomerTuyenHangIdcangDenNavigations)
                    .HasForeignKey(d => d.IdcangDen)
                    .HasConstraintName("FK_tblTuyenDuong_tblDMPort1");

                entity.HasOne(d => d.IdcangDiNavigation).WithMany(p => p.TblDmcustomerTuyenHangIdcangDiNavigations)
                    .HasForeignKey(d => d.IdcangDi)
                    .HasConstraintName("FK_tblTuyenDuong_tblDMPort");

                entity.HasOne(d => d.IddmcustomerNavigation).WithMany(p => p.TblDmcustomerTuyenHangs)
                    .HasForeignKey(d => d.Iddmcustomer)
                    .HasConstraintName("FK_tblTuyenDuong_tblDMCustomer");

                entity.HasOne(d => d.IddmloaiHinhVanChuyenNavigation).WithMany(p => p.TblDmcustomerTuyenHangs)
                    .HasForeignKey(d => d.IddmloaiHinhVanChuyen)
                    .HasConstraintName("FK_tblDMCustomerTuyenHang_tblDMLoaiHinhVanChuyen");

                entity.HasOne(d => d.IdquocGiaDenNavigation).WithMany(p => p.TblDmcustomerTuyenHangIdquocGiaDenNavigations)
                    .HasForeignKey(d => d.IdquocGiaDen)
                    .HasConstraintName("FK_tblTuyenDuong_tblDMCountry1");

                entity.HasOne(d => d.IdquocGiaDiNavigation).WithMany(p => p.TblDmcustomerTuyenHangIdquocGiaDiNavigations)
                    .HasForeignKey(d => d.IdquocGiaDi)
                    .HasConstraintName("FK_tblTuyenDuong_tblDMCountry");
            });

            modelBuilder.Entity<TblDmcustomerType>(entity =>
            {
                entity.ToTable("tblDMCustomerType");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(200);
                entity.Property(e => e.NameEN).HasColumnName("NameEN");
                entity.Property(e => e.NameVI).HasColumnName("NameVI");
            });

            modelBuilder.Entity<TblDmloaiDoanhNghiep>(entity =>
            {
                entity.ToTable("tblDMLoaiDoanhNghiep");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(200);
                entity.Property(e => e.NameEN).HasColumnName("NameEN");
                entity.Property(e => e.NameVI).HasColumnName("NameVI");
            });

            modelBuilder.Entity<TblDmloaiHinhVanChuyen>(entity =>
            {
                entity.ToTable("tblDMLoaiHinhVanChuyen");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.NameEN).HasColumnName("NameEN");
                entity.Property(e => e.NameVI).HasColumnName("NameVI");
            });

            modelBuilder.Entity<TblDmloaiNotification>(entity =>
            {
                entity.ToTable("tblDMLoaiNotification");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.NoiDungEn)
                    .HasMaxLength(500)
                    .HasColumnName("NoiDungEN");
                entity.Property(e => e.NoiDungVi)
                    .HasMaxLength(500)
                    .HasColumnName("NoiDungVI");
                entity.Property(e => e.Tag).HasMaxLength(50);
                entity.Property(e => e.TieuDeEn)
                    .HasMaxLength(500)
                    .HasColumnName("TieuDeEN");
                entity.Property(e => e.TieuDeVi)
                    .HasMaxLength(500)
                    .HasColumnName("TieuDeVI");
            });

            modelBuilder.Entity<TblDmloaiTacNghiep>(entity =>
            {
                entity.ToTable("tblDMLoaiTacNghiep");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Stt).HasColumnName("STT");
                entity.Property(e => e.TenMau).HasMaxLength(200);
            });

            modelBuilder.Entity<TblDmnghiepVu>(entity =>
            {
                entity.ToTable("tblDMNghiepVu");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(200);
                entity.Property(e => e.NameEN).HasColumnName("NameEN");
                entity.Property(e => e.NameVI).HasColumnName("NameVI");
            });

            modelBuilder.Entity<TblDmphanLoaiKhachHang>(entity =>
            {
                entity.ToTable("tblDMPhanLoaiKhachHang");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(200);
                entity.Property(e => e.NameEN).HasColumnName("NameEN");
                entity.Property(e => e.NameVI).HasColumnName("NameVI");
            });

            modelBuilder.Entity<TblDmphongBan>(entity =>
            {
                entity.ToTable("tblDMPhongBan");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FlagPhuTrachJob).HasColumnName("FlagPhuTrachJOB");
                entity.Property(e => e.FlagQuanLyJob).HasColumnName("FlagQuanLyJOB");
                entity.Property(e => e.GhiChu).HasMaxLength(150);
                entity.Property(e => e.IddmvanPhong).HasColumnName("IDDMVanPhong");
                entity.Property(e => e.NameEN)
                    .HasMaxLength(150)
                    .HasColumnName("NameEN");
                entity.Property(e => e.NameVI)
                    .HasMaxLength(150)
                    .HasColumnName("NameVI");
                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.HasOne(d => d.IddmvanPhongNavigation).WithMany(p => p.TblDmphongBans)
                    .HasForeignKey(d => d.IddmvanPhong)
                    .HasConstraintName("FK_tblDMPhongBan_tblDMVanPhong");
            });

            modelBuilder.Entity<TblDmport>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblDMCang");

                entity.ToTable("tblDMPort");

                entity.HasIndex(e => e.Idcity, "iIDCity_tblDMPort");

                entity.HasIndex(e => e.IdquocGia, "iIDQuocGia_tblDMPort");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AddressEN).HasColumnName("AddressEN");
                entity.Property(e => e.AddressVI).HasColumnName("AddressVI");
                entity.Property(e => e.BankAccountNumber).HasMaxLength(150);
                entity.Property(e => e.BankAddress).HasMaxLength(150);
                entity.Property(e => e.BankBranchName).HasMaxLength(150);
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Fax).HasMaxLength(50);
                entity.Property(e => e.Idbank).HasColumnName("IDBank");
                entity.Property(e => e.Idcity).HasColumnName("IDCity");
                entity.Property(e => e.IdquocGia).HasColumnName("IDQuocGia");
                entity.Property(e => e.NameEN).HasColumnName("NameEN");
                entity.Property(e => e.NameVI).HasColumnName("NameVI");
                entity.Property(e => e.Note).HasMaxLength(250);
                entity.Property(e => e.Phone).HasMaxLength(50);
                entity.Property(e => e.SoDuUsd).HasColumnName("SoDuUSD");
                entity.Property(e => e.SoDuVnd).HasColumnName("SoDuVND");
                entity.Property(e => e.TaxCode).HasMaxLength(50);
                entity.Property(e => e.Website).HasMaxLength(100);

                entity.HasOne(d => d.IdbankNavigation).WithMany(p => p.TblDmports)
                    .HasForeignKey(d => d.Idbank)
                    .HasConstraintName("FK_tblDMPort_tblDMBank");

                entity.HasOne(d => d.IdcityNavigation).WithMany(p => p.TblDmports)
                    .HasForeignKey(d => d.Idcity)
                    .HasConstraintName("FK_tblDMCang_tblDMCity");

                entity.HasOne(d => d.IdquocGiaNavigation).WithMany(p => p.TblDmports)
                    .HasForeignKey(d => d.IdquocGia)
                    .HasConstraintName("FK_tblDMPort_tblDMCountry");
            });

            modelBuilder.Entity<TblDmvanPhong>(entity =>
            {
                entity.ToTable("tblDMVanPhong");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AddressEN)
                    .HasMaxLength(250)
                    .HasColumnName("AddressEN");
                entity.Property(e => e.AddressVI)
                    .HasMaxLength(250)
                    .HasColumnName("AddressVI");
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Fax).HasMaxLength(150);
                entity.Property(e => e.Idcity).HasColumnName("IDCity");
                entity.Property(e => e.Idcontact).HasColumnName("IDContact");
                entity.Property(e => e.Idcountry).HasColumnName("IDCountry");
                entity.Property(e => e.Idcurrency).HasColumnName("IDCurrency");
                entity.Property(e => e.IdkeToanTruong).HasColumnName("IDKeToanTruong");
                entity.Property(e => e.Mst)
                    .HasMaxLength(50)
                    .HasColumnName("MST");
                entity.Property(e => e.NameEN)
                    .HasMaxLength(250)
                    .HasColumnName("NameEN");
                entity.Property(e => e.NameVI)
                    .HasMaxLength(250)
                    .HasColumnName("NameVI");
                entity.Property(e => e.Note).HasMaxLength(250);
                entity.Property(e => e.ParentId).HasColumnName("ParentID");
                entity.Property(e => e.Phone).HasMaxLength(150);
                entity.Property(e => e.TaxCode).HasMaxLength(50);
                entity.Property(e => e.Website).HasMaxLength(100);

                entity.HasOne(d => d.IdcityNavigation).WithMany(p => p.TblDmvanPhongs)
                    .HasForeignKey(d => d.Idcity)
                    .HasConstraintName("FK_tblDMVanPhong_tblDMCity");

                entity.HasOne(d => d.IdcontactNavigation).WithMany(p => p.TblDmvanPhongIdcontactNavigations)
                    .HasForeignKey(d => d.Idcontact)
                    .HasConstraintName("FK_tblDMVanPhong_tblNhanSu2");

                entity.HasOne(d => d.IdcountryNavigation).WithMany(p => p.TblDmvanPhongs)
                    .HasForeignKey(d => d.Idcountry)
                    .HasConstraintName("FK_tblDMVanPhong_tblDMCountry");

                entity.HasOne(d => d.IdcurrencyNavigation).WithMany(p => p.TblDmvanPhongs)
                    .HasForeignKey(d => d.Idcurrency)
                    .HasConstraintName("FK_tblDMVanPhong_tblDMCurrency");

                entity.HasOne(d => d.IdkeToanTruongNavigation).WithMany(p => p.TblDmvanPhongIdkeToanTruongNavigations)
                    .HasForeignKey(d => d.IdkeToanTruong)
                    .HasConstraintName("FK_tblDMVanPhong_tblNhanSu1");
            });

            modelBuilder.Entity<TblDmxepLoaiBaoCaoCv>(entity =>
            {
                entity.ToTable("tblDMXepLoaiBaoCaoCV");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TblJobdocument>(entity =>
            {
                entity.ToTable("tblJOBDocument");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(250);
                entity.Property(e => e.EnumLclfcl).HasColumnName("EnumLCLFCl");
                entity.Property(e => e.FlagRpt).HasColumnName("FlagRPT");
                entity.Property(e => e.Idbooking).HasColumnName("IDBooking");
                entity.Property(e => e.IddieuPhoi).HasColumnName("IDDieuPhoi");
                entity.Property(e => e.IddmtoDo).HasColumnName("IDDMToDo");
                entity.Property(e => e.Idjob).HasColumnName("IDJOB");
                entity.Property(e => e.Idjobtodo).HasColumnName("IDJOBTodo");
                entity.Property(e => e.Idrequest).HasColumnName("IDRequest");
                entity.Property(e => e.IdtheoDoiVanChuyen).HasColumnName("IDTheoDoiVanChuyen");
                entity.Property(e => e.IduserUpload).HasColumnName("IDUserUpload");
                entity.Property(e => e.Path).HasMaxLength(250);

                entity.HasOne(d => d.IduserUploadNavigation).WithMany(p => p.TblJobdocuments)
                    .HasForeignKey(d => d.IduserUpload)
                    .HasConstraintName("FK_tblJOBDocument_tblSysUser");
            });

            modelBuilder.Entity<TblJobnotification>(entity =>
            {
                entity.ToTable("tblJOBNotification");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Idjob).HasColumnName("IDJOB");
                entity.Property(e => e.Idobject).HasColumnName("IDObject");
                entity.Property(e => e.NgayThongBao).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblJobuserAccess>(entity =>
            {
                entity.ToTable("tblJOBUserAccess");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Idbooking).HasColumnName("IDBooking");
                entity.Property(e => e.Idjob).HasColumnName("IDJOB");
                entity.Property(e => e.IdjobbaoGia).HasColumnName("IDJOBBaoGia");
                entity.Property(e => e.Idjoborder).HasColumnName("IDJOBOrder");
                entity.Property(e => e.Iduser).HasColumnName("IDUser");
                entity.Property(e => e.IduserGroup).HasColumnName("IDUserGroup");

                entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TblJobuserAccesses)
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("FK_tblJOBUserAccess_tblSysUser");

                entity.HasOne(d => d.IduserGroupNavigation).WithMany(p => p.TblJobuserAccesses)
                    .HasForeignKey(d => d.IduserGroup)
                    .HasConstraintName("FK_tblJOBUserAccess_tblSysUserGroup");
            });

            modelBuilder.Entity<TblJobuserPheDuyet>(entity =>
            {
                entity.ToTable("tblJOBUserPheDuyet");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Idbooking).HasColumnName("IDBooking");
                entity.Property(e => e.IddeNghiTamUng).HasColumnName("IDDeNghiTamUng");
                entity.Property(e => e.IdjobAcoutingRequest).HasColumnName("IDJobAcoutingRequest");
                entity.Property(e => e.IdphieuThuChi).HasColumnName("IDPhieuThuChi");
                entity.Property(e => e.IduserPheDuyet).HasColumnName("IDUserPheDuyet");

                entity.HasOne(d => d.IduserPheDuyetNavigation).WithMany(p => p.TblJobuserPheDuyets)
                    .HasForeignKey(d => d.IduserPheDuyet)
                    .HasConstraintName("FK_tblJOBUserPheDuyet_tblSysUser");
            });

            modelBuilder.Entity<TblNhanSu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblNhanSu_1");

                entity.ToTable("tblNhanSu");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.DateDelete).HasColumnType("datetime");
                entity.Property(e => e.DiDong).HasMaxLength(20);
                entity.Property(e => e.DiaChiHienTai).HasMaxLength(250);
                entity.Property(e => e.DiaChiThuongTru).HasMaxLength(250);
                entity.Property(e => e.EditDate).HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.GioiTinh).HasDefaultValue(0);
                entity.Property(e => e.HoTenEN)
                    .HasMaxLength(50)
                    .HasColumnName("HoTenEN");
                entity.Property(e => e.HoTenVI)
                    .HasMaxLength(50)
                    .HasColumnName("HoTenVI");
                entity.Property(e => e.IdchucVu).HasColumnName("IDChucVu");
                entity.Property(e => e.IdphongBan).HasColumnName("IDPhongBan");
                entity.Property(e => e.IduserDelete).HasColumnName("IDUserDelete");
                entity.Property(e => e.IdvanPhong).HasColumnName("IDVanPhong");
                entity.Property(e => e.Luongcoban).HasColumnName("LUONGCOBAN");
                entity.Property(e => e.Manhansu)
                    .HasMaxLength(10)
                    .HasColumnName("MANHANSU");
                entity.Property(e => e.Mst)
                    .HasMaxLength(50)
                    .HasColumnName("MST");
                entity.Property(e => e.NgayCapCmt).HasColumnName("NgayCapCMT");
                entity.Property(e => e.NgayHetHanBangLai).HasColumnType("datetime");
                entity.Property(e => e.NoiCapCmt)
                    .HasMaxLength(200)
                    .HasColumnName("NoiCapCMT");
                entity.Property(e => e.NoiCapHoChieu).HasMaxLength(200);
                entity.Property(e => e.PhotoUrl).HasColumnName("photoUrl");
                entity.Property(e => e.PictureNv)
                    .HasColumnType("image")
                    .HasColumnName("PictureNV");
                entity.Property(e => e.QueQuan).HasMaxLength(250);
                entity.Property(e => e.SoBangLai).HasMaxLength(50);
                entity.Property(e => e.SoCmt)
                    .HasMaxLength(50)
                    .HasColumnName("SoCMT");
                entity.Property(e => e.SoDuVnd).HasColumnName("SoDuVND");
                entity.Property(e => e.SoHoChieu).HasMaxLength(50);
                entity.Property(e => e.SoHopDong).HasMaxLength(200);
                entity.Property(e => e.SoLuongKh).HasColumnName("SoLuongKH");
                entity.Property(e => e.SoTaiKhoanNh)
                    .HasMaxLength(50)
                    .HasColumnName("SoTaiKhoanNH");
                entity.Property(e => e.SoTruong).HasMaxLength(250);

                entity.HasOne(d => d.IdchucVuNavigation).WithMany(p => p.TblNhanSus)
                    .HasForeignKey(d => d.IdchucVu)
                    .HasConstraintName("FK_tblNhanSu_tblDMChucVu");

                entity.HasOne(d => d.IdphongBanNavigation).WithMany(p => p.TblNhanSus)
                    .HasForeignKey(d => d.IdphongBan)
                    .HasConstraintName("FK_tblNhanSu_tblDMPhongBan");

                entity.HasOne(d => d.IdvanPhongNavigation).WithMany(p => p.TblNhanSus)
                    .HasForeignKey(d => d.IdvanPhong)
                    .HasConstraintName("FK_tblNhanSu_tblDMVanPhong");
            });

            modelBuilder.Entity<TblNhanSuTreelist>(entity =>
            {
                entity.ToTable("tblNhanSuTreelist");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IdnhanVien).HasColumnName("IDNhanVien");
                entity.Property(e => e.NameGroup).HasMaxLength(200);
                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.HasOne(d => d.IdnhanVienNavigation).WithMany(p => p.TblNhanSuTreelists)
                    .HasForeignKey(d => d.IdnhanVien)
                    .HasConstraintName("FK_tblNhanSuTreelist_tblNhanSu");
            });

            modelBuilder.Entity<TblNotification>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblNotification_1");

                entity.ToTable("tblNotification", tb => tb.HasTrigger("tgNotification_Insert"));

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Cd)
                    .HasColumnType("datetime")
                    .HasColumnName("cd");
                entity.Property(e => e.GuidId).HasColumnName("GuidID");
                entity.Property(e => e.KieuDoiTuongLienQuan)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ListDoiTuongLienQuan)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Ud)
                    .HasColumnType("datetime")
                    .HasColumnName("ud");
            });

            modelBuilder.Entity<TblSysCode>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblSysOptionCongViec");

                entity.ToTable("tblSysCode");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CodeFormat).HasMaxLength(100);
                entity.Property(e => e.DateCreate).HasColumnType("datetime");
                entity.Property(e => e.DateEdit).HasColumnType("datetime");
                entity.Property(e => e.EnumNgayTinhStt).HasColumnName("EnumNgayTinhSTT");
                entity.Property(e => e.EnumTangSttcustomer).HasColumnName("EnumTangSTTCustomer");
                entity.Property(e => e.EnumTangSttloaiHinh).HasColumnName("EnumTangSTTLoaiHinh");
                entity.Property(e => e.EnumTangStttgian).HasColumnName("EnumTangSTTTgian");
                entity.Property(e => e.IduserCreate).HasColumnName("IDUserCreate");
                entity.Property(e => e.IduserEdit).HasColumnName("IDUserEdit");
                entity.Property(e => e.NgayBd).HasColumnName("NgayBD");
                entity.Property(e => e.NgayBdapDung)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayBDApDung");
                entity.Property(e => e.SttbatDau).HasColumnName("STTBatDau");
            });

            modelBuilder.Entity<TblSysCodeKey>(entity =>
            {
                entity.ToTable("tblSysCodeKey");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Format)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.IdsysCode).HasColumnName("IDSysCode");
                entity.Property(e => e.KeyCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.IdsysCodeNavigation).WithMany(p => p.TblSysCodeKeys)
                    .HasForeignKey(d => d.IdsysCode)
                    .HasConstraintName("FK_tblSysCodeKey_tblSysCode");
            });

            modelBuilder.Entity<TblSysCodeKyHieu>(entity =>
            {
                entity.ToTable("tblSysCodeKyHieu");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.EnumLoaiKh).HasColumnName("EnumLoaiKH");
                entity.Property(e => e.IdsysCode).HasColumnName("IDSysCode");
                entity.Property(e => e.Kh)
                    .HasMaxLength(50)
                    .HasColumnName("KH");

                entity.HasOne(d => d.IdsysCodeNavigation).WithMany(p => p.TblSysCodeKyHieus)
                    .HasForeignKey(d => d.IdsysCode)
                    .HasConstraintName("FK_tblSysCodeKyHieu_tblSysCode");
            });

            modelBuilder.Entity<TblSysConfig>(entity =>
            {
                entity.ToTable("tblSysConfig");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Address).HasColumnName("ADDRESS");
                entity.Property(e => e.Appkey)
                    .HasMaxLength(50)
                    .HasColumnName("APPKEY");
                entity.Property(e => e.Customername)
                    .HasMaxLength(250)
                    .HasColumnName("CUSTOMERNAME");
                entity.Property(e => e.CustomerNameVI)
                    .HasMaxLength(250)
                    .HasColumnName("CUSTOMERNameVI");
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.Flag).HasColumnName("FLAG");
                entity.Property(e => e.Flaguse).HasColumnName("FLAGUSE");
                entity.Property(e => e.HotLine).HasMaxLength(250);
                entity.Property(e => e.LetterHead).HasColumnType("image");
                entity.Property(e => e.Logo).HasColumnType("image");
                entity.Property(e => e.Mst)
                    .HasMaxLength(250)
                    .HasColumnName("MST");
                entity.Property(e => e.ReportHeader).HasColumnType("image");
                entity.Property(e => e.Serverip)
                    .HasMaxLength(50)
                    .HasColumnName("SERVERIP");
                entity.Property(e => e.Telfax)
                    .HasMaxLength(250)
                    .HasColumnName("TELFAX");
                entity.Property(e => e.Website).HasMaxLength(250);
            });

            modelBuilder.Entity<TblSysFcmtoken>(entity =>
            {
                entity.ToTable("tblSysFCMToken");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Fcmtoken)
                    .HasMaxLength(250)
                    .HasColumnName("FCMToken");
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User).WithMany(p => p.TblSysFcmtokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblSysFCMToken_tblNhanSu");
            });

            modelBuilder.Entity<TblSysOption>(entity =>
            {
                entity.ToTable("tblSysOption");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.EnumJobid).HasColumnName("EnumJOBID");
                entity.Property(e => e.EnumKieuTangStt).HasColumnName("EnumKieuTangSTT");
                entity.Property(e => e.EnumShowJobinfo).HasColumnName("EnumShowJOBInfo");
                entity.Property(e => e.Jobidformat)
                    .HasMaxLength(50)
                    .HasColumnName("JOBIDFormat");
                entity.Property(e => e.Language).HasDefaultValue(0);
                entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
                entity.Property(e => e.NgayBd).HasColumnName("NgayBD");
                entity.Property(e => e.NgayKt).HasColumnName("NgayKT");
                entity.Property(e => e.SoLuongKh).HasColumnName("SoLuongKH");
                entity.Property(e => e.SttbatDau).HasColumnName("STTBatDau");
                entity.Property(e => e.UsddecimalCal).HasColumnName("USDDecimalCal");
            });

            modelBuilder.Entity<TblSysUser>(entity =>
            {
                entity.ToTable("tblSysUser");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Active).HasDefaultValue(false);
                entity.Property(e => e.GhiChu).HasMaxLength(250);
                entity.Property(e => e.IdgroupUser).HasColumnName("IDGroupUser");
                entity.Property(e => e.IdnhanVien).HasColumnName("IDNhanVien");
                entity.Property(e => e.Password).HasMaxLength(50);
                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.IdgroupUserNavigation).WithMany(p => p.TblSysUsers)
                    .HasForeignKey(d => d.IdgroupUser)
                    .HasConstraintName("FK_tblSysUser_tblSysUserGroup");

                entity.HasOne(d => d.IdnhanVienNavigation).WithMany(p => p.TblSysUsers)
                    .HasForeignKey(d => d.IdnhanVien)
                    .HasConstraintName("FK_tblSysUser_tblNhanSu");
            });

            modelBuilder.Entity<TblSysUserGroup>(entity =>
            {
                entity.ToTable("tblSysUserGroup");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.GhiChu).HasMaxLength(150);
                entity.Property(e => e.GroupName).HasMaxLength(100);
            });

            modelBuilder.Entity<TblSysUserNotification>(entity =>
            {
                entity.ToTable("tblSysUserNotification");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FlagNewJob).HasColumnName("FlagNewJOB");
                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TblSysUserNotifications)
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("FK_tblSysUserNotification_tblSysUser");
            });

            modelBuilder.Entity<TblSysUserRelationGroup>(entity =>
            {
                entity.ToTable("tblSysUserRelationGroup");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Idgroup).HasColumnName("IDGroup");
                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.HasOne(d => d.IdgroupNavigation).WithMany(p => p.TblSysUserRelationGroups)
                    .HasForeignKey(d => d.Idgroup)
                    .HasConstraintName("FK_tblSysUserRelationGroup_tblSysUserGroup");

                entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TblSysUserRelationGroups)
                    .HasForeignKey(d => d.Iduser)
                    .HasConstraintName("FK_tblSysUserRelationGroup_tblSysUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
