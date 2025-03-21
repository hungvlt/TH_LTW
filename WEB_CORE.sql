USE WEB_CORE

SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (Id, Name) VALUES
(1, 'Cà Phê'),
(2, 'Trà'),
(6, N'Bánh mì'),
(7, 'Sữa & Đá Xay'),
(8, 'Bánh Ngọt & Tráng Miệng'),
(9, 'Yogurt & Smoothie')
SET IDENTITY_INSERT Categories OFF;

SET IDENTITY_INSERT Products ON;
INSERT INTO Products (Id, Name, Price, Description, ImageUrl, CategoryId) VALUES
-- Cà Phê
(1, N'Cà Phê Đen', 25000, N'Cà phê nguyên chất pha phin', '/images/capheden.jpg', 1),
(2, N'Cà Phê Sữa', 30000, N'Cà phê sữa đá truyền thống', '/images/caphesua.jpg', 1),
(3, N'Bạc Xỉu', 35000, N'Cà phê sữa đặc ít cà phê', '/images/bacxiu.jpg', 1),

-- Trà
(4, 'Trà Đào Cam Sả', 40000, 'Trà đào kết hợp cam sả', '/images/tradaocamsa.jpg', 2),
(5, 'Trà Xanh Matcha', 45000, 'Trà xanh Nhật Bản, vị thanh mát', '/images/traxanhmatcha.jpg', 2),

-- Nước Giải Khát
(6, 'Soda Chanh Dây', 35000, 'Soda mát lạnh kết hợp vị chanh dây', '/images/sodachanhday.jpg', 3),
(7, 'Nước Cam Ép', 40000, 'Nước cam tươi nguyên chất', '/images/nuoccamep.jpg', 3),

-- Snack Nhẹ
(8, 'Bánh Mì Nướng Muối Ớt', 30000, 'Bánh mì giòn rụm với muối ớt cay cay', '/images/banhminuong.jpg', 4),
(9, 'Khoai Tây Chiên', 35000, 'Khoai tây chiên giòn thơm, ăn kèm tương', '/images/khoaitaychien.jpg', 4),

-- Các Loại Hạt	
(10, 'Hạt Dưa', 20000, 'Hạt dưa rang truyền thống, thơm bùi', '/images/hatdua.jpg', 5),
(11, 'Hạt Hướng Dương', 25000, 'Hạt hướng dương giòn tan, ăn vui miệng', '/images/hathuongduong.jpg', 5),

-- Cà Phê
(12, N'Cà phê Espresso', 35000, N'Cà phê đậm đặc, hương vị mạnh mẽ', '/images/cafe_espresso.jpg', 1),
(13, N'Cà phê Cappuccino', 45000, N'Cà phê sữa bọt mịn, phong cách Ý', '/images/cafe_cappuccino.jpg', 1),
(14, N'Cà phê Mocha', 50000, N'Cà phê sô-cô-la hấp dẫn', '/images/cafe_mocha.jpg', 1),
(15, N'Cà phê Latte', 48000, N'Cà phê pha với sữa nóng', '/images/cafe_latte.jpg', 1),

-- Trà
(16, N'Trà sữa trân châu', 45000, N'Trà sữa truyền thống kèm trân châu dai ngon', '/images/tra_sua_tran_chau.jpg', 2),
(17, N'Trà hoa cúc mật ong', 40000, N'Trà hoa cúc kết hợp với mật ong ngọt dịu', '/images/tra_hoa_cuc_mat_ong.jpg', 2),
(18, N'Trà ô long', 42000, N'Trà ô long thanh mát, tốt cho sức khỏe', '/images/tra_o_long.jpg', 2),
(19, N'Trà chanh sả gừng', 38000, N'Trà chanh ấm, thơm vị sả gừng', '/images/tra_chanh_sa_gung.jpg', 2),

-- Nước Giải Khát
(20, N'Nước ép dưa hấu', 38000, N'Nước ép dưa hấu tươi mát', '/images/nuoc_ep_dua_hau.jpg', 3),
(21, N'Nước dừa tươi', 35000, N'Nước dừa nguyên chất, giải nhiệt mùa hè', '/images/nuoc_dua_tuoi.jpg', 3),
(22, N'Sinh tố bơ', 45000, N'Sinh tố bơ sánh mịn, béo ngậy', '/images/sinh_to_bo.jpg', 3),
(23, N'Sinh tố dâu', 47000, N'Sinh tố dâu tây chua ngọt hài hòa', '/images/sinh_to_dau.jpg', 3),

-- Snack Nhẹ
(24, N'Bánh bao kim sa', 30000, N'Bánh bao nhân trứng muối chảy tan chảy', '/images/banh_bao_kim_sa.jpg', 4),
(25, N'Bánh flan caramel', 25000, N'Bánh flan mềm mịn, thơm ngon', '/images/banh_flan.jpg', 4),
(26, N'Bánh crepe xoài', 35000, N'Bánh crepe nhân xoài tươi', '/images/banh_crepe_xoai.jpg', 4),
(27, N'Khoai lang lắc phô mai', 32000, N'Khoai lang giòn, lắc phô mai béo ngậy', '/images/khoai_lang_lac.jpg', 4),

-- Các Loại Hạt	
(28, N'Hạt điều rang muối', 50000, N'Hạt điều rang giòn rụm', '/images/hat_dieu.jpg', 5),
(29, N'Hạnh nhân mật ong', 55000, N'Hạnh nhân tẩm mật ong thơm ngon', '/images/hanh_nhan_mat_ong.jpg', 5),
(30, N'Hạt óc chó Mỹ', 60000, N'Hạt óc chó giàu dinh dưỡng, tốt cho trí não', '/images/hat_oc_cho.jpg', 5),
(31, N'Hạt dẻ cười', 58000, N'Hạt dẻ cười giòn, béo ngậy', '/images/hat_de_cuoi.jpg', 5),

-- Bánh Mì (CategoryId = 6)
(32, N'Bánh mì thịt nướng', 40000, N'Bánh mì kẹp thịt nướng, rau thơm và sốt đặc biệt', '/images/banh_mi_thit_nuong.jpg', 6),
(33, N'Bánh mì ốp la', 35000, N'Bánh mì giòn với trứng ốp la và pate', '/images/banh_mi_op_la.jpg', 6),
(34, N'Bánh mì chả cá', 38000, N'Bánh mì với chả cá nóng hổi, sốt cay', '/images/banh_mi_cha_ca.jpg', 6),
(35, N'Bánh mì gà xé', 37000, N'Bánh mì kẹp gà xé phay, rau thơm', '/images/banh_mi_ga_xe.jpg', 6),
(36, N'Bánh mì pate', 32000, N'Bánh mì giòn, pate thơm béo', '/images/banh_mi_pate.jpg', 6),
(37, N'Bánh mì xíu mại', 40000, N'Bánh mì ăn kèm xíu mại sốt cà chua', '/images/banh_mi_xiu_mai.jpg', 6),
(38, N'Bánh mì heo quay', 45000, N'Bánh mì kẹp thịt heo quay giòn bì', '/images/banh_mi_heo_quay.jpg', 6),
(39, N'Bánh mì bò bít tết', 50000, N'Bánh mì kèm bò bít tết, trứng và pate', '/images/banh_mi_bo_bit_tet.jpg', 6),
(40, N'Bánh mì không', 15000, N'Bánh mì giòn, không nhân, thích hợp ăn kèm', '/images/banh_mi_khong.jpg', 6),

-- Sữa & Đồ Uống Đá Xay (CategoryId = 7)
(41, N'Matcha Latte', 45000, N'Sữa tươi kết hợp với matcha nguyên chất', '/images/matchalatte.jpg', 7),
(42, N'Chocolate Đá Xay', 50000, N'Chocolate nguyên chất xay cùng đá mát lạnh', '/images/chocolatedaxay.jpg', 7),
(43, N'Caramel Macchiato', 55000, N'Cà phê espresso pha cùng caramel và sữa tươi', '/images/caramelmacchiato.jpg', 7),

-- Bánh Ngọt & Tráng Miệng (CategoryId = 8)
(44, N'Cheesecake', 60000, N'Bánh cheesecake mềm mịn với lớp phô mai thơm ngon', '/images/cheesecake.jpg', 8),
(45, N'Tiramisu', 65000, N'Bánh tiramisu đậm đà hương vị cà phê và cacao', '/images/tiramisu.jpg', 8),
(46, N'Bông Lan Trứng Muối', 50000, N'Bánh bông lan mềm xốp với trứng muối và sốt bơ', '/images/bonglantrungmuoi.jpg', 8),

-- Yogurt & Smoothie (CategoryId = 9)
(47, N'Sữa Chua Đá Xay', 40000, N'Sữa chua kết hợp đá xay mát lạnh', '/images/suachuadaxay.jpg', 9),
(48, N'Sinh Tố Bơ', 45000, N'Sinh tố bơ béo ngậy, thơm ngon', '/images/sinhtobo.jpg', 9),
(49, N'Sinh Tố Dâu', 45000, N'Sinh tố dâu tươi kết hợp với sữa chua', '/images/sinhtodau.jpg', 9),

-- Đồ Uống Healthy (CategoryId = 10)
(50, N'Nước Ép Cần Tây', 50000, N'Nước ép cần tây tươi giúp thanh lọc cơ thể', '/images/nuocep_cantay.jpg', 10),
(51, N'Detox Smoothie', 55000, N'Hỗn hợp trái cây detox giúp giải độc và đẹp da', '/images/detoxsmoothie.jpg', 10),
(52, N'Trà Thảo Mộc', 45000, N'Trà từ các loại thảo mộc giúp thư giãn', '/images/trathaomoc.jpg', 10),

-- Thức Uống Đặc Biệt (CategoryId = 11)
(53, N'Cà Phê Trứng', 60000, N'Cà phê kết hợp với trứng tạo lớp kem mịn', '/images/caphtrung.jpg', 11),
(54, N'Cà Phê Dừa', 55000, N'Cà phê hoà quyện với nước cốt dừa béo ngậy', '/images/caphdua.jpg', 11),
(55, N'Trà Đào Hạt Chia', 50000, N'Trà đào kết hợp với hạt chia bổ dưỡng', '/images/tradaohatchia.jpg', 11);

SET IDENTITY_INSERT Products OFF;

SET IDENTITY_INSERT ProductImages ON;
INSERT INTO ProductImages (Id, Url, ProductId) VALUES
(1, '/images/capheden_1.jpg', 1),
(2, '/images/caphesua_1.jpg', 2),
(3, '/images/bacxiu_1.jpg', 3),
(4, '/images/tradaocamsa_1.jpg', 4),
(5, '/images/traxanhmatcha_1.jpg', 5),
(6, '/images/sodachanhday_1.jpg', 6),
(7, '/images/nuoccamep_1.jpg', 7),
(8, '/images/banhminuong_1.jpg', 8),
(9, '/images/khoaitaychien_1.jpg', 9),
(10, '/images/hatdua_1.jpg', 10),
(11, '/images/hathuongduong_1.jpg', 11),
(12, '/images/cafe_espresso.jpg', 12),
(13, '/images/cafe_cappuccino.jpg', 13),
(14, '/images/cafe_mocha.jpg', 14),
(15, '/images/cafe_latte.jpg', 15),
(16, '/images/tra_sua_tran_chau.jpg', 16),
(17, '/images/tra_hoa_cuc_mat_ong.jpg', 17),
(18, '/images/tra_o_long.jpg', 18),
(19, '/images/tra_chanh_sa_gung.jpg', 19),
(20, '/images/nuoc_ep_dua_hau.jpg', 20),
(21, '/images/nuoc_dua_tuoi.jpg', 21),
(22, '/images/sinh_to_bo.jpg', 22),
(23, '/images/sinh_to_dau.jpg', 23),
(24, '/images/banh_bao_kim_sa.jpg', 24),
(25, '/images/banh_flan.jpg', 25),
(26, '/images/banh_crepe_xoai.jpg', 26),
(27, '/images/khoai_lang_lac.jpg', 27),
(28, '/images/hat_dieu.jpg', 28),
(29, '/images/hanh_nhan_mat_ong.jpg', 29),
(30, '/images/hat_oc_cho.jpg', 30),
(31, '/images/hat_de_cuoi.jpg', 31),
(32, '/images/banh_mi_thit_nuong.jpg', 32),
(33, '/images/banh_mi_op_la.jpg', 33),
(34, '/images/banh_mi_cha_ca.jpg', 34),
(35, '/images/banh_mi_ga_xe.jpg', 35),
(36, '/images/banh_mi_pate.jpg', 36),
(37, '/images/banh_mi_xiu_mai.jpg', 37),
(38, '/images/banh_mi_heo_quay.jpg', 38),
(39, '/images/banh_mi_bo_bit_tet.jpg', 39),
(40, '/images/banh_mi_khong.jpg', 40),
(41, '/images/matchalatte.jpg', 41),
(42, '/images/chocolatedaxay.jpg', 42),
(43, '/images/caramelmacchiato.jpg', 43),
(44, '/images/cheesecake.jpg', 44),
(45, '/images/tiramisu.jpg', 45),
(46, '/images/bonglantrungmuoi.jpg', 46),
(47, '/images/suachuadaxay.jpg', 47),
(48, '/images/sinhtobo.jpg', 48),
(49, '/images/sinhtodau.jpg', 49),
(50, '/images/nuocep_cantay.jpg', 50),
(51, '/images/detoxsmoothie.jpg', 51),
(52, '/images/trathaomoc.jpg', 52),
(53, '/images/caphtrung.jpg', 53),
(54, '/images/caphdua.jpg', 54),
(55, '/images/tradaohatchia.jpg', 55);
SET IDENTITY_INSERT ProductImages OFF;

SET IDENTITY_INSERT Users ON;
INSERT INTO Users (ID, Name, Email, Password, Role) VALUES
(1, N'Thế Hùng', 'hungvu01235@gmail.com', 123, 'Admin'),
(2, N'Người dùng 1', 'vidu123@gmail.com', 123, 'User')
SET IDENTITY_INSERT Users OFF;

SELECT * FROM Products
SELECT * FROM ProductImages
SELECT * FROM Categories
SELECT * FROM AspNetUsers