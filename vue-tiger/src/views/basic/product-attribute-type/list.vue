<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.title" placeholder="请输入查询关键字" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />

      <el-button-group>
        <el-button v-waves class="filter-item" size="mini" type="primary" icon="el-icon-search" @click="handleFilter">
          搜索
        </el-button>
        <!-- <el-button size="mini" type="primary" icon="el-icon-arrow-down" @click="handleSearch" /> -->
      </el-button-group>

      <el-button class="filter-item" size="mini" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        添加
      </el-button>

      <el-button v-waves :loading="downloadLoading" class="filter-item" size="mini" icon="el-icon-download" @click="handleImport">
        导入
      </el-button>

      <el-dropdown>
        <el-button size="mini">
          批量操作<i class="el-icon-arrow-down el-icon--right" />
        </el-button>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item>
            <el-link icon="el-icon-edit">审核</el-link>
          </el-dropdown-item>
          <el-dropdown-item><el-link icon="el-icon-delete">删除</el-link></el-dropdown-item>
          <el-dropdown-item>
            <el-link v-waves :loading="downloadLoading" class="filter-item" size="mini" icon="el-icon-download" @click="handleDownload">导出
            </el-link>
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <div v-show="searchDivVisibilty" class="search-container">
        <el-form ref="searchForm" :model="listQuery" label-width="80px">
          <el-row>
            <el-col :span="6">
              <el-form-item v-model="listQuery.title" label="名称" placeholder="请输入名称" label-width="120px" class="postInfo-container-item">
                <el-input v-model="listQuery.title" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="重要性" label-width="120px" class="postInfo-container-item">
                <el-select v-model="listQuery.importance" placeholder="重要性" clearable style="width: 90px" class="filter-item">
                  <el-option v-for="item in importanceOptions" :key="item" :label="item" :value="item" />
                </el-select>
              </el-form-item>
            </el-col>

            <el-col :span="6">
              <el-form-item label="类型" label-width="120px" class="postInfo-container-item">
                <el-select v-model="listQuery.type" placeholder="类型" clearable class="filter-item" style="width: 130px">
                  <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name+'('+item.key+')'" :value="item.key" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="排序" label-width="120px" class="postInfo-container-item">
                <el-select v-model="listQuery.sort" style="width: 140px" class="filter-item" @change="handleFilter">
                  <el-option v-for="item in sortOptions" :key="item.key" :label="item.label" :value="item.key" />
                </el-select>
              </el-form-item>
            </el-col>

          </el-row>

          <el-form-item label="" label-width="120px">
            <el-button type="primary" @click="handleFilter">
              搜索
            </el-button>
            <el-button type="primary" @click="resetSearchForm('searchForm')">
              重置
            </el-button>
            <el-button type="primary" @click="handleSearch">
              关闭
            </el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>

    <el-table v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%" @sort-change="sortChange">
      <el-table-column align="center" type="selection" width="55" />

      <!-- <el-table-column align="center" label="ID" width="80" prop="id" sortable="custom">
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column> -->

      <el-table-column min-width="300px" label="名称">
        <template slot-scope="{row}">
          <router-link :to="'/basic/product/attribute/'+row.id" class="link-type">
            <span>{{ row.name }}</span>
          </router-link>
        </template>
      </el-table-column>
      <!-- <el-table-column width="120px" align="center" label="合并一列">
        <template slot-scope="scope">
          <span>{{ scope.row.title }}</span>
          <span>{{ scope.row.author }}</span>
          <span><img :src="scope.row.image_uri" width="100px"></span>
        </template>
      </el-table-column> -->

      <el-table-column width="120px" align="center" label="属性数量">
        <template slot-scope="scope">
          <span>{{ scope.row.attributeCount }}</span>
        </template>
      </el-table-column>

      <el-table-column width="120px" align="center" label="参数数量">
        <template slot-scope="scope">
          <span>{{ scope.row.paramCount }}</span>
        </template>
      </el-table-column>

      <el-table-column width="180px" align="center" label="创建时间" sortable>
        <template slot-scope="scope">
          <span>{{ scope.row.creationTime | formatDate }}</span>
        </template>
      </el-table-column>

      <!-- <el-table-column width="100px" label="重要性">
        <template slot-scope="scope">
          <svg-icon v-for="n in +scope.row.importance" :key="n" icon-class="star" class="meta-item__icon" />
        </template>
      </el-table-column> -->

      <el-table-column align="center" label="操作" width="400">
        <template slot-scope="scope">

          <el-button size="mini">
            <router-link :to="'/basic/product-attribute/list'" class="link-type">
              属性列表
            </router-link>
          </el-button>

          <el-button type="primary" size="mini" icon="el-icon-edit" @click="handleUpdate(scope.row)">
            编辑
          </el-button>
          <el-button type="danger" size="mini" icon="el-icon-delete" @click="handleDelete(scope.row)">
            删除
          </el-button>
        </template>

      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="120px" style="width: 400px; margin-left:50px;">

        <el-form-item label="名称" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          取消
        </el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">
          确认
        </el-button>
      </div>
    </el-dialog>

    <!-- 搜索过滤 -->
    <el-dialog title="搜索条件" :visible.sync="searchFilterDialogVisible" width="80%">
      <!-- <el-divider /> -->
      <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
      <el-row style="margin-top:10px;">只能上传Excel文件,文件大小不能超过10M</el-row>
      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="importExcelDialogVisible = false">
          关闭
        </el-button>
      </div>
    </el-dialog>

    <!-- 导入excel -->
    <el-dialog title="导入" :visible.sync="importExcelDialogVisible" width="650px">
      <el-button v-waves :loading="downloadLoading" size="mini" icon="el-icon-download" @click="handleDownload">
        下载模板
      </el-button>
      <el-divider />
      <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
      <el-row style="margin-top:10px;">只能上传Excel文件,文件大小不能超过10M</el-row>
      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="importExcelDialogVisible = false">
          关闭
        </el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import { getProductAttributeTypes, createProductAttributeType, updateProductAttributeType, deleteProductAttributeType } from '@/api/basic/productAttributeType'
import waves from '@/directive/waves' // waves directive
import { parseTime } from '@/utils'
import UploadExcelComponent from '@/components/UploadExcel/index.vue'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

const calendarTypeOptions = [
  { key: 'show', display_name: '显示' },
  { key: 'hidden', display_name: '隐藏' }
]

// arr to obj, such as { CN : "China", US : "USA" }
const calendarTypeKeyValue = calendarTypeOptions.reduce((acc, cur) => {
  acc[cur.key] = cur.display_name
  return acc
}, {})

export default {
  name: 'ProductAttributeType',
  components: { Pagination, UploadExcelComponent },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: 'success',
        draft: 'info',
        deleted: 'danger'
      }
      return statusMap[status]
    },
    typeFilter(type) {
      return calendarTypeKeyValue[type]
    }
  },
  data() {
    return {
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10
      },
      importanceOptions: [1, 2, 3],
      // statusOptions: [true, false],
      calendarTypeOptions,
      sortOptions: [{ label: '升序', key: '+id' }, { label: '降序', key: '-id' }],
      statusOptions: ['published', 'draft', 'deleted'],
      showReviewer: false,
      show: true,
      temp: {
        id: undefined,
        name: ''
      },
      searchDivVisible: false,
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: 'Edit',
        create: 'Create'
      },
      searchDivVisibilty: false,
      searchFilterDialogVisible: false,
      importExcelDialogVisible: false,
      dialogPvVisible: false,
      pvData: [],
      rules: {
        type: [{ required: true, message: 'type is required', trigger: 'change' }],
        timestamp: [{ type: 'date', required: true, message: 'timestamp is required', trigger: 'change' }],
        name: [{ required: true, message: '请输入规格名称', trigger: 'blur' }]
      },
      downloadLoading: false
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getProductAttributeTypes(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    handleFilter() {
      this.searchDivVisibilty = false
      this.listQuery.page = 1
      this.getList()
    },
    handleModifyStatus(row, status) {
      this.$message({
        message: '操作成功',
        type: 'success'
      })
      row.status = status
    },
    // 排序
    sortChange(data) {
      const { prop, order } = data
      if (prop === 'id') {
        this.sortByID(order)
      }
    },
    sortByID(order) {
      if (order === 'ascending') {
        this.listQuery.sort = '+id'
      } else {
        this.listQuery.sort = '-id'
      }
      this.handleFilter()
    },
    resetSearchForm(formName) {
      console.log('formName', formName)
      this.$refs[formName].resetFields()
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        name: ''
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
          this.temp.id = parseInt(Math.random() * 100) + 1024 // mock a id
          this.temp.author = 'vue-element-admin'
          createProductAttributeType(this.temp).then(() => {
            this.list.unshift(this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '创建成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.temp.timestamp = new Date(this.temp.timestamp)
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
          tempData.timestamp = +new Date(tempData.timestamp) // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
          updateProductAttributeType(tempData).then(() => {
            for (const v of this.list) {
              if (v.id === this.temp.id) {
                const index = this.list.indexOf(v)
                this.list.splice(index, 1, this.temp)
                break
              }
            }
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
    handleDelete(row) {
      this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deleteProductAttributeType(row.id)
          .then((response) => {
            const index = this.list.findIndex((v) => v.id === row.id)
            this.list.splice(index, 1)
            this.$notify({
              title: '成功',
              message: '删除成功',
              type: 'success',
              duration: 2000
            })
          })
          .catch((err) => {
            console.log(err)
          })
      }).catch((err) => {
        console.log(err)
      })
    },

    handleSearch() {
      this.searchDivVisibilty = !this.searchDivVisibilty
      console.log('handleSearch', this.searchDivVisibilty)
    },
    handleImport() {
      this.importExcelDialogVisible = true
      console.log('导入数据')
    },
    beforeUpload(file) {
      const isLt10M = file.size / 1024 / 1024 < 10

      if (isLt10M) {
        return true
      }

      this.$message({
        message: '请不要上传大于10MB的文件',
        type: 'warning'
      })
      return false
    },
    handleSuccess({ results, header }) {
      this.$message({
        message: '文件上传成功',
        type: 'success'
      })
      // this.tableData = results
      // this.tableHeader = header
    },
    handleDownload() {
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['timestamp', 'title', 'type', 'importance', 'status']
        const filterVal = ['timestamp', 'title', 'type', 'importance', 'status']
        const data = this.formatJson(filterVal, this.list)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: '商品列表' + (new Date()).toLocaleDateString()
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v => filterVal.map(j => {
        if (j === 'timestamp') {
          return parseTime(v[j])
        } else {
          return v[j]
        }
      }))
    }

  }
}
</script>

<style scoped>
.edit-input {
  padding-right: 100px;
}
.cancel-btn {
  position: absolute;
  right: 15px;
  top: 10px;
}
.filter-container{
  margin-bottom: 20px;
  position:relative;

}
.search-container{
  position:absolute;
  border:rgb(230, 217, 217) solid 1px;
  padding:30px 20px;
  left:0px;
  top:40px;
  width:100%;
  /* height:500px; */
  background-color:rgb(255, 255, 255);
  z-index:99;
  display:block;
}

</style>

