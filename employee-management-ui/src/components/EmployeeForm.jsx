import React, { useState, useEffect } from 'react';
import { X, Save, User, Mail, Phone, Briefcase, Calendar, DollarSign } from 'lucide-react';

const EmployeeForm = ({ employee, onSubmit, onClose }) => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    department: '',
    jobTitle: '',
    dateOfBirth: '',
    dateOfJoining: '',
    salary: ''
  });

  useEffect(() => {
    if (employee) {
      setFormData({
        ...employee,
        dateOfBirth: employee.dateOfBirth.split('T')[0],
        dateOfJoining: employee.dateOfJoining.split('T')[0],
        salary: employee.salary.toString()
      });
    }
  }, [employee]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit({
      ...formData,
      salary: parseFloat(formData.salary)
    });
  };

  return (
    <div style={{ position: 'fixed', top: 0, left: 0, right: 0, bottom: 0, backgroundColor: 'rgba(15, 23, 42, 0.7)', backdropFilter: 'blur(4px)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 1000, padding: '1rem' }}>
      <div className="glass-card fade-in" style={{ maxWidth: '600px', width: '100%', position: 'relative', overflow: 'hidden' }}>
        <button 
          style={{ position: 'absolute', right: '1.5rem', top: '1.5rem', background: 'none', color: 'var(--text-muted)' }}
          onClick={onClose}
        >
          <X size={24} />
        </button>

        <h2 style={{ marginBottom: '0.5rem', display: 'flex', alignItems: 'center', gap: '0.75rem' }}>
          {employee ? 'Edit Employee' : 'Add New Employee'}
        </h2>
        <p style={{ color: 'var(--text-muted)', marginBottom: '2rem', fontSize: '0.875rem' }}>
          {employee ? 'Update the details for existing employee record.' : 'Enter information to create a new employee profile.'}
        </p>

        <form onSubmit={handleSubmit}>
          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.25rem' }}>
            <div className="form-group">
              <label>First Name</label>
              <input name="firstName" value={formData.firstName} onChange={handleChange} required placeholder="John" />
            </div>
            <div className="form-group">
              <label>Last Name</label>
              <input name="lastName" value={formData.lastName} onChange={handleChange} required placeholder="Doe" />
            </div>
          </div>

          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.25rem' }}>
            <div className="form-group">
              <label>Email Address</label>
              <input type="email" name="email" value={formData.email} onChange={handleChange} required placeholder="john.doe@company.com" />
            </div>
            <div className="form-group">
              <label>Phone Number</label>
              <input name="phone" value={formData.phone} onChange={handleChange} required placeholder="+1-555-0101" />
            </div>
          </div>

          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.25rem' }}>
            <div className="form-group">
              <label>Department</label>
              <select name="department" value={formData.department} onChange={handleChange} required>
                <option value="">Select Department</option>
                <option value="Engineering">Engineering</option>
                <option value="HR">HR</option>
                <option value="Marketing">Marketing</option>
                <option value="Sales">Sales</option>
                <option value="Finance">Finance</option>
                <option value="Quality Assurance">Quality Assurance</option>
              </select>
            </div>
            <div className="form-group">
              <label>Job Title</label>
              <input name="jobTitle" value={formData.jobTitle} onChange={handleChange} required placeholder="Software Engineer" />
            </div>
          </div>

          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.25rem' }}>
            <div className="form-group">
              <label>Date of Birth</label>
              <input type="date" name="dateOfBirth" value={formData.dateOfBirth} onChange={handleChange} required />
            </div>
            <div className="form-group">
              <label>Date of Joining</label>
              <input type="date" name="dateOfJoining" value={formData.dateOfJoining} onChange={handleChange} required />
            </div>
          </div>

          <div className="form-group">
            <label>Salary (USD)</label>
            <div style={{ position: 'relative' }}>
              <DollarSign size={16} style={{ position: 'absolute', left: '12px', top: '50%', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
              <input type="number" name="salary" value={formData.salary} onChange={handleChange} required step="0.01" style={{ paddingLeft: '2.5rem' }} placeholder="75000.00" />
            </div>
          </div>

          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: '1rem', marginTop: '1rem' }}>
            <button type="button" className="btn-secondary" onClick={onClose}>Cancel</button>
            <button type="submit" className="btn-primary" style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
              <Save size={18} />
              {employee ? 'Update Employee' : 'Create Employee'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default EmployeeForm;
