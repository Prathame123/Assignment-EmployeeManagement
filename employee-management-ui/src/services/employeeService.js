import axios from 'axios';

const api = axios.create({
  baseURL: '/api/employees',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const employeeService = {
  // Get all employees
  getAll: async () => {
    const response = await api.get('');
    return response.data;
  },

  // Get employee by ID
  getById: async (id) => {
    const response = await api.get(`/${id}`);
    return response.data;
  },

  // Add new employee
  create: async (employee) => {
    const response = await api.post('', employee);
    return response.data;
  },

  // Update employee
  update: async (id, employee) => {
    const response = await api.put(`/${id}`, employee);
    return response.data;
  },

  // Delete employee
  delete: async (id) => {
    await api.delete(`/${id}`);
  },
};
