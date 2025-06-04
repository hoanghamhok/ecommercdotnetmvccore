import axios from 'axios';

const API = axios.create({
  baseURL: "http://localhost:5091/api"
})

//API lấy danh sách nhóm sản phẩm
export const fetchCategories = () => API.get('/categories');

//API tạo mới nhóm sản phẩm
export const createCategory = (
  data: {name: String, description: String}) => API.post(
    '/categories', data);

//API sửa nhóm sản phẩm
export const updateCategory = (id: number, 
  data : {name: String, description: String}) => API.put(
    `/categories/${id}`, data);

//API xóa nhóm sản phẩm
export const deleteCategory = (id: number) => API.delete(
  `/categories/${id}`);




// PRODUCT API
export const fetchProducts = () => API.get("/products");
export const createProduct = (data: any) =>
                  API.post("/products", data);
export const updateProduct = (id: number, data: any) => 
                  API.put(`/products/${id}`, data);
export const deleteProduct = (id: number) => 
                  API.delete(`/products/${id}`);

export default API;