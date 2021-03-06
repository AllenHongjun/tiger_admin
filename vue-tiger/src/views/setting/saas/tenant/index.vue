<template>
  <div class="app-container">
    <el-row style="margin-bottom: 20px">
      <el-input v-model="listQuery.Filter" placeholder="关键词" style="width: 150px" class="filter-item" />

      <el-button size="small" class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        搜索
      </el-button>
      <el-button type="primary" size="small" icon="el-icon-edit" @click="handleCreate">
        添加
      </el-button>
    </el-row>
    <el-table v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row>
      <el-table-column label="名称" align="left">
        <template slot-scope="scope">
          {{ scope.row.name }}
        </template>
      </el-table-column>

      <el-table-column align="center" label="操作" width="400">
        <template slot-scope="scope">
          <el-button type="info" size="mini" @click="handleFeather(scope.row)">
            功能
          </el-button>
          <el-button type="primary" size="mini" @click="handleUpdate(scope.row)">
            编辑
          </el-button>
          <el-button size="mini" type="danger" @click="deleteData(scope.row.id)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="fetchData" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-width="180px" label-position="top">
        <el-form-item label="名称" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item label="版本">
          <el-select v-model="temp.version" placeholder="版本">
            <el-option label="商业版" value="business" />
            <el-option label="企业版" value="enterprise" />
            <el-option label="专业版" value="professional" />
            <el-option label="标准版" value="standard" />
          </el-select>
        </el-form-item>
        <el-form-item v-if="dialogStatus === 'create'" label="管理员邮箱" prop="adminEmailAddress">
          <el-input v-model="temp.adminEmailAddress" type="password" />
        </el-form-item>
        <el-form-item v-if="dialogStatus === 'create'" label="管理员密码" prop="adminPassword">
          <el-input v-model="temp.adminPassword" type="password" />
        </el-form-item>
        <!-- <el-form-item label="使用共享数据库" prop="title">
          <el-checkbox v-model="checked"></el-checkbox>
        </el-form-item> -->
      </el-form>
      <div style="text-align: right">
        <el-button type="danger" @click="dialogFormVisible = false">取消</el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">确认</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getTenantList,
  deleteTenant,
  createTenant,
  updateTenant
} from '@/api/user'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination

export default {
  name: 'Tenant',
  components: { Pagination },
  props: {
    providerName: {
      type: String,
      required: false
    }
  },
  data() {
    return {
      list: null,
      listLoading: true,
      total: 0,
      listQuery: {
        page: 1,
        limit: 20,
        SkipCount: 0,
        Sorting: 'name desc',
        Filter: ''
      },
      checked: true,

      temp: {
        id: '',
        name: '',
        version: [],
        adminPassword: '',
        adminEmailAddress: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: '编辑',
        create: '添加'
      },
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
        adminEmailAddress: [
          { required: true, message: '请输入邮箱', trigger: 'blur' }
        ],
        adminPassword: [
          { required: true, message: '请输入密码', trigger: 'blur' }
        ]
      }
    }
  },
  created() {
    this.fetchData()
  },
  methods: {
    fetchData() {
      this.listLoading = true
      getTenantList(this.listQuery).then((response) => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    handleFilter() {
      this.listQuery.page = 1
      this.fetchData()
    },
    resetTemp() {
      this.temp = {
        name: '',
        adminEmailAddress: '',
        adminPassword: ''
      }
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          createTenant(this.temp).then(() => {
            this.list.unshift(this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '租户添加成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      // this.temp.timestamp = new Date(this.temp.timestamp)
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          // console.log(tempData)
          // tempData.timestamp = +new Date(tempData.timestamp) // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
          updateTenant(tempData.id, tempData).then(() => {
            const index = this.list.findIndex((v) => v.id === this.temp.id)
            this.list.splice(index, 1, this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '修改成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    deleteData(id) {
      console.log('delete')
      this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          deleteTenant(id)
            .then((response) => {
              const index = this.list.findIndex((v) => v.id === id)
              this.list.splice(index, 1)
              this.$message({
                message: '删除成功',
                type: 'success'
              })
            })
            .catch((err) => {
              console.log(err)
            })
        })
        .catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          })
        })
    },
    handleFeather(row) {
      this.$message('功能管理待开发')
    }
  }
}
</script>
