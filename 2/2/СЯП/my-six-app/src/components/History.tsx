import React from 'react';

interface HistoryProps {
  history: string[];
}

const History: React.FC<HistoryProps> = ({ history }) => {
  return (
    <div className="mt-4 p-4 bg-gray-100 rounded-lg dark:bg-gray-900">
      <h3 className="text-lg font-semibold mb-2">История</h3>
      <ul className="space-y-1 max-h-40 overflow-y-auto">
        {history.map((entry, index) => (
          <li key={index} className="text-sm opacity-75">{entry}</li>
        ))}
      </ul>
    </div>
  );
};

export default History;