﻿using System;
using System.Collections.Generic;
using UnityMvvmToolkit.Common.Interfaces;
using UnityMvvmToolkit.Common.Internal;

namespace UnityMvvmToolkit.Common
{
    public abstract class BindableVisualElement : IBindableVisualElement
    {
        private readonly List<string> _bindableProperties;
        private readonly IPropertyProvider _propertyProvider;
        private readonly BindingStringParser _bindingStringParser;

        protected BindableVisualElement(IPropertyProvider propertyProvider)
        {
            _propertyProvider = propertyProvider;
            _bindableProperties = new List<string>();
            _bindingStringParser = new BindingStringParser();
        }

        public IEnumerable<string> BindableProperties => _bindableProperties;

        public abstract void UpdateValues();

        protected IProperty<TValueType> GetProperty<TValueType>(string bindingStringData)
        {
            var bindingData = _bindingStringParser.GetBindingData(bindingStringData.AsMemory());
            var propertyName = bindingData.PropertyName.ToString();

            var property = _propertyProvider.GetProperty<TValueType>(propertyName, bindingData.ConverterName);
            if (property != null)
            {
                _bindableProperties.Add(propertyName);
            }

            return property;
        }

        protected IReadOnlyProperty<TValueType> GetReadOnlyProperty<TValueType>(string bindingStringData)
        {
            var bindingData = _bindingStringParser.GetBindingData(bindingStringData.AsMemory());
            var propertyName = bindingData.PropertyName.ToString();

            var property = _propertyProvider.GetReadOnlyProperty<TValueType>(propertyName, bindingData.ConverterName);
            if (property != null)
            {
                _bindableProperties.Add(propertyName);
            }

            return property;
        }
    }
}