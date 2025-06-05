'use client'

import React, {useEffect, useState} from 'react'
import { fetchCategories, createCategory, updateCategory, deleteCategory } from '@/app/services/api'

export default function Category(){
  const [categories, setCategories] = useState<any[]>([]);
  const [name, setName] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [editCategoryId, setEditCategoryId] 
      = useState<number | null>(null);

  useEffect( () => {
    fetchCategories().then((res) => {
      console.log("API Response:", res.data); // Debug
      setCategories(res.data);
    })
    .catch((err) => console.error(
        'Lỗi khi tải danh sách nhóm sản phẩm', err));
  }, []);
  
  // Xử lý khi nhấn vào nút thêm nhóm sản phẩm
  const handleCreateCategory = () => {
    // Kiểm tra dữ liệu đầu vào
    if (!name.trim() || !description.trim()) {
      alert('Vui lòng nhập đầy đủ tên và mô tả nhóm sản phẩm');
      return;
    }

    const categoryData = {
      name: name.trim(),
      description: description.trim()
    };

    createCategory(categoryData)
      .then((res) => {
        console.log('Tạo thành công:', res.data);
        // Thêm category mới vào danh sách
        setCategories([...categories, res.data]);
        // Reset form
        setName('');
        setDescription('');
        alert('Thêm nhóm sản phẩm thành công!');
      })
      .catch((err) => {
        console.error('Lỗi khi tạo nhóm sản phẩm:', err);
        console.error('Chi tiết lỗi:', err.response?.data);
        alert('Có lỗi xảy ra khi thêm nhóm sản phẩm!');
      });
  }

  //Xử lý khi nhấn vào nút cập nhật nhóm sản phẩm
  const handleUpdateCategory = () => {
    // Kiểm tra dữ liệu đầu vào
    if (!name.trim() || !description.trim()) {
      alert('Vui lòng nhập đầy đủ tên và mô tả nhóm sản phẩm');
      return;
    }

    if (!isEditing || !editCategoryId) {
      alert('Không có nhóm sản phẩm nào được chọn để cập nhật');
      return;
    }

    const categoryData = {
      name: name.trim(),
      description: description.trim()
    };

    updateCategory(editCategoryId, categoryData)
      .then((res) => {
        console.log('Cập nhật thành công:', res.data);
        // Cập nhật category trong danh sách
        const updatedCategories = categories.map((category) =>
          category.id === editCategoryId
            ? { ...category, name: name.trim(), description: description.trim() }
            : category
        );
        setCategories(updatedCategories);
        
        // Reset form và trạng thái editing
        setName('');
        setDescription(''); 
        setIsEditing(false);
        setEditCategoryId(null);
        alert('Cập nhật nhóm sản phẩm thành công!');
      })
      .catch((err) => {
        console.error('Lỗi khi cập nhật nhóm sản phẩm:', err);
        console.error('Chi tiết lỗi:', err.response?.data);
        alert('Có lỗi xảy ra khi cập nhật nhóm sản phẩm!');
      });
  }

  //Xử lý khi nhấn vào nút xóa nhóm sản phẩm
  const handleDeleteCategory = (id: number) => {
    // Xác nhận trước khi xóa
    const confirmDelete = window.confirm(
      'Bạn có chắc chắn muốn xóa nhóm sản phẩm này?\nHành động này không thể hoàn tác!'
    );
    
    if (!confirmDelete) return;
    
    console.log("Đang xóa category với ID:", id); // Debug
    
    deleteCategory(id)
      .then((res) => {
        console.log('Xóa thành công:', res);
        // Loại bỏ category khỏi danh sách
        setCategories(categories.filter(
              (category) => category.id !== id));
        
        // Nếu đang edit category này thì reset form
        if (editCategoryId === id) {
          setName('');
          setDescription('');
          setIsEditing(false);
          setEditCategoryId(null);
        }
        
        alert('Xóa nhóm sản phẩm thành công!');
      })
      .catch((err) => {
        console.error('Lỗi khi xóa nhóm sản phẩm:', err);
        console.error('Chi tiết lỗi:', err.response?.data);
        console.error('Status:', err.response?.status);
        
        // Hiển thị lỗi cụ thể cho người dùng
        let errorMessage = 'Có lỗi xảy ra khi xóa nhóm sản phẩm!';
        if (err.response?.status === 400) {
          errorMessage = 'Không thể xóa nhóm sản phẩm này. Có thể nhóm đang được sử dụng bởi các sản phẩm khác.';
        } else if (err.response?.status === 404) {
          errorMessage = 'Nhóm sản phẩm không tồn tại hoặc đã bị xóa.';
        }
        alert(errorMessage);
      });
  }

  //Xử lý khi nhấn nút Sửa
  const handleEditCategory = (category : any) => {
    console.log('Đang edit category:', category); // Debug
    setIsEditing(true);
    setEditCategoryId(category.id);
    setName(category.name);
    setDescription(category.description);
  }

  // Hàm hủy chỉnh sửa
  const handleCancelEdit = () => {
    setName('');
    setDescription('');
    setIsEditing(false);
    setEditCategoryId(null);
  }

  return (
    <div>
      <h1 className='text-2xl font-bold mb-4'>QUẢN LÝ NHÓM SẢN PHẨM</h1>

      {/* Form thêm, sửa nhóm sản phẩm */}
      <div className='mb-4'>
        <input
          type='text'
          className='border p-2 mr-2'
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder='Tên nhóm sản phẩm'
        />
        <input
          type='text'
          className='border p-2 mr-2'
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder='Mô tả nhóm sản phẩm'
        />
        { isEditing ? (
          <>
            <button onClick={handleUpdateCategory} 
                className='bg-green-500 text-white px-4 py-2 mr-2'>
              Cập nhật
            </button>
            <button onClick={handleCancelEdit} 
                className='bg-gray-500 text-white px-4 py-2'>
              Hủy
            </button>
          </>
        ):(
          <button onClick={handleCreateCategory} 
              className='bg-blue-500 text-white px-4 py-2'>
            Thêm nhóm sản phẩm
          </button>
        )
        }
      </div>

      {/* Danh sách nhóm sản phẩm */}
      <div className='mt-4'>
        <h2 className='font-semibold'>Danh sách nhóm sản phẩm</h2>
        {categories.length === 0 ? (
          <p className='text-gray-500 mt-2'>Chưa có nhóm sản phẩm nào.</p>
        ) : (
          <ul>
            {categories.map((category, index) => (
              <li key={category.id || `category-${index}`} 
                className='flex justify-between items-center border-b py-2'>
                  <div>
                    <span className='font-medium'>{category.name}</span>
                    <p className='text-sm text-gray-600'>
                      {category.description}
                    </p>
                  </div>
                  <div className='flex gap-2'>
                    <button onClick={() => handleEditCategory(category)}
                        className='text-blue-500 hover:underline px-2 py-1'>
                      Sửa
                    </button>
                    <button onClick={() => handleDeleteCategory(category.id)}
                        className='text-red-500 hover:underline px-2 py-1'>
                      Xóa
                    </button>
                  </div>
              </li>
            ))}
          </ul>
        )}
      </div>

    </div>
  )
}