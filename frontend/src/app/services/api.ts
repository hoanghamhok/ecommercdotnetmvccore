import axios from 'axios';

const API = axios.create({
  baseURL: "http://localhost:5091/api"
})

//API lấy danh sách nhóm sản phẩm
export const fetchCategories = () => API.get('/categories');

//API tạo mới nhóm sản phẩm
export const createCategory = (
  data: {name: string, description: string}) => API.post(
    '/categories', data);

//API sửa nhóm sản phẩm
export const updateCategory = (id: number, 
  data : {name: string, description: string}) => API.put(
    `/categories/${id}`, data);

//API xóa nhóm sản phẩm
export const deleteCategory = (id: number) => API.delete(
  `/categories/${id}`);

export default API;