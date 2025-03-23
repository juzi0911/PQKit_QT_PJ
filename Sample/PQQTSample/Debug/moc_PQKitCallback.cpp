/****************************************************************************
** Meta object code from reading C++ file 'PQKitCallback.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.12.8)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../PQKitCallback.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'PQKitCallback.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.12.8. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
struct qt_meta_stringdata_CPQKitCallback_t {
    QByteArrayData data[17];
    char stringdata0[220];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_CPQKitCallback_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_CPQKitCallback_t qt_meta_stringdata_CPQKitCallback = {
    {
QT_MOC_LITERAL(0, 0, 14), // "CPQKitCallback"
QT_MOC_LITERAL(1, 15, 22), // "signalInitializeResult"
QT_MOC_LITERAL(2, 38, 0), // ""
QT_MOC_LITERAL(3, 39, 7), // "lResult"
QT_MOC_LITERAL(4, 47, 17), // "signalLoginResult"
QT_MOC_LITERAL(5, 65, 12), // "i_nLoginType"
QT_MOC_LITERAL(6, 78, 19), // "signalElementPickup"
QT_MOC_LITERAL(7, 98, 9), // "i_ulObjID"
QT_MOC_LITERAL(8, 108, 6), // "LPWSTR"
QT_MOC_LITERAL(9, 115, 14), // "i_wsEntityName"
QT_MOC_LITERAL(10, 130, 13), // "i_nEntityType"
QT_MOC_LITERAL(11, 144, 22), // "signalElementSelection"
QT_MOC_LITERAL(12, 167, 11), // "i_wObjNames"
QT_MOC_LITERAL(13, 179, 12), // "i_wFaceNames"
QT_MOC_LITERAL(14, 192, 7), // "double*"
QT_MOC_LITERAL(15, 200, 11), // "i_dPointXYZ"
QT_MOC_LITERAL(16, 212, 7) // "i_nSize"

    },
    "CPQKitCallback\0signalInitializeResult\0"
    "\0lResult\0signalLoginResult\0i_nLoginType\0"
    "signalElementPickup\0i_ulObjID\0LPWSTR\0"
    "i_wsEntityName\0i_nEntityType\0"
    "signalElementSelection\0i_wObjNames\0"
    "i_wFaceNames\0double*\0i_dPointXYZ\0"
    "i_nSize"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_CPQKitCallback[] = {

 // content:
       8,       // revision
       0,       // classname
       0,    0, // classinfo
       4,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       4,       // signalCount

 // signals: name, argc, parameters, tag, flags
       1,    1,   34,    2, 0x06 /* Public */,
       4,    1,   37,    2, 0x06 /* Public */,
       6,    3,   40,    2, 0x06 /* Public */,
      11,    4,   47,    2, 0x06 /* Public */,

 // signals: parameters
    QMetaType::Void, QMetaType::Long,    3,
    QMetaType::Void, QMetaType::Int,    5,
    QMetaType::Void, QMetaType::ULong, 0x80000000 | 8, QMetaType::Int,    7,    9,   10,
    QMetaType::Void, 0x80000000 | 8, 0x80000000 | 8, 0x80000000 | 14, QMetaType::Int,   12,   13,   15,   16,

       0        // eod
};

void CPQKitCallback::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        auto *_t = static_cast<CPQKitCallback *>(_o);
        Q_UNUSED(_t)
        switch (_id) {
        case 0: _t->signalInitializeResult((*reinterpret_cast< long(*)>(_a[1]))); break;
        case 1: _t->signalLoginResult((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 2: _t->signalElementPickup((*reinterpret_cast< ulong(*)>(_a[1])),(*reinterpret_cast< LPWSTR(*)>(_a[2])),(*reinterpret_cast< int(*)>(_a[3]))); break;
        case 3: _t->signalElementSelection((*reinterpret_cast< LPWSTR(*)>(_a[1])),(*reinterpret_cast< LPWSTR(*)>(_a[2])),(*reinterpret_cast< double*(*)>(_a[3])),(*reinterpret_cast< int(*)>(_a[4]))); break;
        default: ;
        }
    } else if (_c == QMetaObject::IndexOfMethod) {
        int *result = reinterpret_cast<int *>(_a[0]);
        {
            using _t = void (CPQKitCallback::*)(long );
            if (*reinterpret_cast<_t *>(_a[1]) == static_cast<_t>(&CPQKitCallback::signalInitializeResult)) {
                *result = 0;
                return;
            }
        }
        {
            using _t = void (CPQKitCallback::*)(int );
            if (*reinterpret_cast<_t *>(_a[1]) == static_cast<_t>(&CPQKitCallback::signalLoginResult)) {
                *result = 1;
                return;
            }
        }
        {
            using _t = void (CPQKitCallback::*)(unsigned long , LPWSTR , int );
            if (*reinterpret_cast<_t *>(_a[1]) == static_cast<_t>(&CPQKitCallback::signalElementPickup)) {
                *result = 2;
                return;
            }
        }
        {
            using _t = void (CPQKitCallback::*)(LPWSTR , LPWSTR , double * , int );
            if (*reinterpret_cast<_t *>(_a[1]) == static_cast<_t>(&CPQKitCallback::signalElementSelection)) {
                *result = 3;
                return;
            }
        }
    }
}

QT_INIT_METAOBJECT const QMetaObject CPQKitCallback::staticMetaObject = { {
    &QObject::staticMetaObject,
    qt_meta_stringdata_CPQKitCallback.data,
    qt_meta_data_CPQKitCallback,
    qt_static_metacall,
    nullptr,
    nullptr
} };


const QMetaObject *CPQKitCallback::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *CPQKitCallback::qt_metacast(const char *_clname)
{
    if (!_clname) return nullptr;
    if (!strcmp(_clname, qt_meta_stringdata_CPQKitCallback.stringdata0))
        return static_cast<void*>(this);
    if (!strcmp(_clname, "CPQKitCallbackBase"))
        return static_cast< CPQKitCallbackBase*>(this);
    return QObject::qt_metacast(_clname);
}

int CPQKitCallback::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 4)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 4;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 4)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 4;
    }
    return _id;
}

// SIGNAL 0
void CPQKitCallback::signalInitializeResult(long _t1)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(&_t1)) };
    QMetaObject::activate(this, &staticMetaObject, 0, _a);
}

// SIGNAL 1
void CPQKitCallback::signalLoginResult(int _t1)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(&_t1)) };
    QMetaObject::activate(this, &staticMetaObject, 1, _a);
}

// SIGNAL 2
void CPQKitCallback::signalElementPickup(unsigned long _t1, LPWSTR _t2, int _t3)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(&_t1)), const_cast<void*>(reinterpret_cast<const void*>(&_t2)), const_cast<void*>(reinterpret_cast<const void*>(&_t3)) };
    QMetaObject::activate(this, &staticMetaObject, 2, _a);
}

// SIGNAL 3
void CPQKitCallback::signalElementSelection(LPWSTR _t1, LPWSTR _t2, double * _t3, int _t4)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(&_t1)), const_cast<void*>(reinterpret_cast<const void*>(&_t2)), const_cast<void*>(reinterpret_cast<const void*>(&_t3)), const_cast<void*>(reinterpret_cast<const void*>(&_t4)) };
    QMetaObject::activate(this, &staticMetaObject, 3, _a);
}
QT_WARNING_POP
QT_END_MOC_NAMESPACE
