namespace vsl_crm_api.Models.Queries
{
    public class CustomerQuery
    {
        // Pagination
        public int Start { get; set; }
        public int Size { get; set; }

        // Customer info
        public long? IDLoaiDoanhNghiep { get; set; }
        public long? IDNghiepVu { get; set; }
        public long? IDPhanLoaiKhachHang { get; set; }
        public long? IDDanhGia { get; set; }
        public long? IDLoaiTacNghiep { get; set; }
        public string? Name { get; set; }
        public string? TaxCode { get; set; }
        public required string ListType { get; set; }


        // Route info
        public long? IDQuocGiaDiTuyenHang { get; set; }
        public long? IDQuocGiaDenTuyenHang { get; set; }
        public long? IDCangDiTuyenHang { get; set; }
        public long? IDCangDenTuyenHang { get; set; }

        // ImEx info
        public long? IDQuocGiaDiXNK { get; set; }
        public long? IDQuocGiaDenXNK { get; set; }
        public long? IDCangDiXNK { get; set; }
        public long? IDCangDenXNK { get; set; }
        public string? Term { get; set; }
        public string? HSCode { get; set; }
        public string? Type { get; set; }
    }
}
