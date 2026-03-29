import React from 'react';
import { Edit2, Trash2, Mail, Phone, Briefcase, Calendar } from 'lucide-react';
import { format } from 'date-fns';

const EmployeeList = ({ employees, onEdit, onDelete }) => {
  if (employees.length === 0) {
    return (
      <div style={{ textAlign: 'center', padding: '3rem', color: 'var(--text-muted)' }}>
        <p>No employees found matching your search.</p>
      </div>
    );
  }

  return (
    <div style={{ overflowX: 'auto' }}>
      <table>
        <thead>
          <tr>
            <th>Employee</th>
            <th>Contact</th>
            <th>Department & Role</th>
            <th>Joined</th>
            <th>Salary</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {employees.map((emp) => (
            <tr key={emp.employeeID} className="fade-in">
              <td>
                <div style={{ display: 'flex', alignItems: 'center', gap: '0.75rem' }}>
                  <div style={{ width: '40px', height: '40px', borderRadius: '50%', backgroundColor: 'var(--primary)', color: 'white', display: 'flex', alignItems: 'center', justifyContent: 'center', fontWeight: 'bold' }}>
                    {emp.firstName[0]}{emp.lastName[0]}
                  </div>
                  <div>
                    <div style={{ fontWeight: '600' }}>{emp.fullName}</div>
                    <div style={{ fontSize: '0.75rem', color: 'var(--text-muted)' }}>ID: {emp.employeeID.substring(0, 8)}...</div>
                  </div>
                </div>
              </td>
              <td>
                <div style={{ fontSize: '0.875rem', display: 'flex', flexDirection: 'column', gap: '0.25rem' }}>
                  <div style={{ display: 'flex', alignItems: 'center', gap: '0.4rem' }}>
                    <Mail size={14} color="var(--text-muted)" /> {emp.email}
                  </div>
                  <div style={{ display: 'flex', alignItems: 'center', gap: '0.4rem' }}>
                    <Phone size={14} color="var(--text-muted)" /> {emp.phone}
                  </div>
                </div>
              </td>
              <td>
                <div style={{ fontSize: '0.875rem' }}>
                  <div style={{ fontWeight: '500' }}>{emp.jobTitle}</div>
                  <div style={{ fontSize: '0.75rem', color: '#6366f1', textTransform: 'uppercase', letterSpacing: '0.05em' }}>{emp.department}</div>
                </div>
              </td>
              <td>
                <div style={{ fontSize: '0.875rem', display: 'flex', alignItems: 'center', gap: '0.4rem' }}>
                  <Calendar size={14} color="var(--text-muted)" />
                  {format(new Date(emp.dateOfJoining), 'MMM d, yyyy')}
                </div>
              </td>
              <td>
                <div style={{ fontWeight: '600', color: 'var(--success)' }}>
                  ${emp.salary.toLocaleString(undefined, { minimumFractionDigits: 2 })}
                </div>
              </td>
              <td>
                <div style={{ display: 'flex', gap: '0.5rem' }}>
                  <button 
                    className="btn-secondary" 
                    style={{ padding: '0.5rem', borderRadius: '6px' }}
                    onClick={() => onEdit(emp)}
                    title="Edit Employee"
                  >
                    <Edit2 size={16} />
                  </button>
                  <button 
                    className="btn-secondary" 
                    style={{ padding: '0.5rem', borderRadius: '6px', color: 'var(--danger)', borderColor: 'rgba(239, 68, 68, 0.2)' }}
                    onClick={() => onDelete(emp.employeeID)}
                    title="Delete Employee"
                  >
                    <Trash2 size={16} />
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default EmployeeList;
