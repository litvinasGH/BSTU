import React from 'react';

interface DisplayProps {
  expression: string;
  result: string;
}

const Display: React.FC<DisplayProps> = ({ expression, result }) => {
  return (
    <div className="p-4 mb-4 bg-gray-100 rounded-lg dark:bg-gray-900 min-h-20">
      <div className="text-gray-600 dark:text-gray-400 text-right text-sm h-6 overflow-hidden">
        {expression || " "}
      </div>
      <div className="text-2xl font-semibold text-right overflow-hidden">
        {result || "0"}
      </div>
    </div>
  );
};

export default Display;