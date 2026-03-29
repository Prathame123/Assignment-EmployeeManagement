import React, { useState, useEffect } from 'react';
import { UserPlus, Users, Search, RefreshCw, AlertCircle } from 'lucide-react';
import EmployeeList from './components/EmployeeList';
import EmployeeForm from './components/EmployeeForm';
import { employeeService } from './services/employeeService';

function App() {
  const [employees, setEmployees] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingEmployee, setEditingEmployee] = useState(null);
  const [searchTerm, setSearchTerm] = useState('');

  const fetchEmployees = async () => {
    try {
      setLoading(true);
      const data = await employeeService.getAll();
      setEmployees(data);
      setError(null);
    } catch (err) {
      console.error('Error fetching employees:', err);
      setError('Failed to load employees. Is the backend running?');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchEmployees();
  }, []);

  const handleAddClick = () => {
    setEditingEmployee(null);
    setIsModalOpen(true);
  };

  const handleEditClick = (employee) => {
    setEditingEmployee(employee);
    setIsModalOpen(true);
  };

  const handleDeleteClick = async (id) => {
    if (window.confirm('Are you sure you want to delete this employee?')) {
      try {
        await employeeService.delete(id);
        setEmployees(employees.filter(emp => emp.employeeID !== id));
      } catch (err) {
        alert('Failed to delete employee');
      }
    }
  };

  const handleFormSubmit = async (formData) => {
    try {
      if (editingEmployee) {
        const updated = await employeeService.update(editingEmployee.employeeID, {
          ...formData,
          employeeID: editingEmployee.employeeID,
          isActive: editingEmployee.isActive
        });
        setEmployees(employees.map(emp => emp.employeeID === updated.employeeID ? updated : emp));
      } else {
        const created = await employeeService.create(formData);
        setEmployees([...employees, created]);
      }
      setIsModalOpen(false);
    } catch (err) {
      const msg = err.response?.data?.message || err.response?.data?.errors?.join(', ') || 'Operation failed';
      alert(msg);
    }
  };

  const filteredEmployees = employees.filter(emp => 
    emp.fullName.toLowerCase().includes(searchTerm.toLowerCase()) ||
    emp.email.toLowerCase().includes(searchTerm.toLowerCase()) ||
    emp.department.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="app-container">
      <header className="fade-in" style={{ marginBottom: '2rem', display: 'flex', justifyContent: 'space-between', alignItems: 'center', color: 'white' }}>
        <div>
          <h1 style={{ fontSize: '2.5rem' }}>Employee Microservices</h1>
          <p style={{ opacity: 0.8 }}>Human Resources Management Dashboard</p>
        </div>
        <button className="btn-primary" style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }} onClick={handleAddClick}>
          <UserPlus size={18} />
          Add Employee
        </button>
      </header>

      <main className="glass-card fade-in" style={{ padding: '1.5rem' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '1.5rem', gap: '1rem' }}>
          <div style={{ position: 'relative', flex: 1 }}>
            <Search size={18} style={{ position: 'absolute', left: '12px', top: '50%', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
            <input 
              type="text" 
              placeholder="Search by name, email, or department..." 
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              style={{ paddingLeft: '2.5rem', backgroundColor: '#fcfcfc' }}
            />
          </div>
          <button className="btn-secondary" style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }} onClick={fetchEmployees} disabled={loading}>
            <RefreshCw size={18} className={loading ? 'spin' : ''} />
            Refresh
          </button>
        </div>

        {error && (
          <div style={{ padding: '1rem', backgroundColor: '#fef2f2', borderRadius: '8px', border: '1px solid #fecaca', color: '#dc2626', display: 'flex', alignItems: 'center', gap: '0.75rem', marginBottom: '1.5rem' }}>
            <AlertCircle size={20} />
            {error}
          </div>
        )}

        {loading && employees.length === 0 ? (
          <div style={{ textAlign: 'center', padding: '4rem', color: 'var(--text-muted)' }}>
            <RefreshCw size={48} className="spin" style={{ marginBottom: '1rem', opacity: 0.3 }} />
            <p>Gathering workforce data...</p>
          </div>
        ) : (
          <EmployeeList 
            employees={filteredEmployees} 
            onEdit={handleEditClick} 
            onDelete={handleDeleteClick} 
          />
        )}
      </main>

      {isModalOpen && (
        <EmployeeForm 
          employee={editingEmployee} 
          onSubmit={handleFormSubmit} 
          onClose={() => setIsModalOpen(false)} 
        />
      )}

      <style>{`
        .spin { animation: spin 1s linear infinite; }
        @keyframes spin { from { transform: rotate(0deg); } to { transform: rotate(360deg); } }
      `}</style>
    </div>
  );
}

export default App;
