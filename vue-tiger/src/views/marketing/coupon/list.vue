<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.title" placeholder="请输入查询关键字" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <!-- <el-select v-model="listQuery.importance" placeholder="条件" clearable style="width: 90px" class="filter-item">
        <el-option v-for="item in importanceOptions" :key="item" :label="item" :value="item" />
      </el-select> -->
      <!-- <el-select v-model="listQuery.status" placeholder="条件" clearable style="width: 90px" class="filter-item">
        <el-option v-for="item in statusOptions" :key="item" :label="item" :value="item" />
      </el-select> -->
      <el-select v-model="listQuery.type" placeholder="状态" clearable class="filter-item" style="width: 130px">
        <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name" :value="item.key" />
      </el-select>
      <el-cascader :props="listQuery.props" />
      <!-- <el-select v-model="listQuery.sort" style="width: 140px" class="filter-item" @change="handleFilter">
        <el-option v-for="item in sortOptions" :key="item.key" :label="item.label" :value="item.key" />
      </el-select> -->

      <el-button-group>
        <el-button v-waves class="filter-item" size="mini" type="primary" icon="el-icon-search" @click="handleFilter">
          搜索
        </el-button>
        <el-button size="mini" type="primary" icon="el-icon-arrow-down" @click="handleSearch" />
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

    <el-table v-loading="listLoading" :data="list" fit highlight-current-row style="width: 100%" @sort-change="sortChange">
      <el-table-column align="center" type="selection" width="55" />

      <!-- <el-table-column align="center" label="ID" width="80" prop="id" sortable="custom">
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column> -->

      <el-table-column min-width="120" label="名称">
        <template slot-scope="{row}">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>

      <el-table-column min-width="80" label="兑换码">
        <template slot-scope="{row}">
          <span>{{ row.code }}</span>
        </template>
      </el-table-column>

      <el-table-column class-name="status-col" label="类型" width="110">
        <template slot-scope="{row}">
          <!-- <el-tag :type="row.type | couponTypeFilter">
            {{ row.type }}
          </el-tag> -->

          <el-tag>{{ row.type | couponTypeFilter }}</el-tag>
        </template>
      </el-table-column>

      <el-table-column width="80px" align="center" label="面值">
        <template slot-scope="scope">
          <span>{{ scope.row.amount }}</span>
        </template>
      </el-table-column>

      <!-- <el-table-column class-name="status-col" label="领取方式" width="110">
        <template slot-scope="{row}">
          <el-tag :type="row.status | statusFilter">
            {{ row.status }}
          </el-tag>
        </template>
      </el-table-column> -->

      <el-table-column width="260px" align="center" label="领取时间" sortable>
        <template slot-scope="scope">
          <span>{{ scope.row.startTime | formatDate("yyyy-MM-DD") }}</span> -
          <span>{{ scope.row.endTime | formatDate("yyyy-MM-DD") }}</span>
        </template>
      </el-table-column>

      <!-- <el-table-column width="120px" align="center" label="使用时间" sortable>
        <template slot-scope="scope">
          <span>{{ scope.row.timestamp | formatDate }}</span>
        </template>
      </el-table-column> -->

      <el-table-column min-width="80" width="120px" label="发布数量">
        <template slot-scope="{row}">
          <span style="color:red;">发布: {{ row.count }}</span> <br>
          <span>剩余: {{ row.count - row.useCount }}</span>
        </template>
      </el-table-column>

      <el-table-column align="center" label="操作" width="300">
        <template slot-scope="scope">
          <el-button type="info" size="mini" @click="handleHistory(scope.row)">
            领取记录
          </el-button>

          <el-button type="primary" size="mini" @click="handleUpdate(scope.row)">
            编辑
          </el-button>
          <el-button type="danger" size="mini" @click="handleDelete(scope.row)">
            删除
          </el-button>

        </template>

      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="120px" style="margin-left:50px;">
        <el-form-item label="优惠券类型">
          <el-select v-model="temp.type" class="filter-item" placeholder="Please select">
            <el-option v-for="item in couponTypeOptions" :key="item.key" :label="item.display_name" :value="item.key" />
          </el-select>
        </el-form-item>

        <el-form-item label="标题" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>

        <el-form-item label="兑换码" prop="code">
          <el-input v-model="temp.code" />
        </el-form-item>

        <el-form-item label="总发行量" prop="count">
          <el-input v-model="temp.count" type="number" />
        </el-form-item>

        <el-form-item label="面额" prop="amount">
          <el-input v-model="temp.amount" type="number">
            <template slot="append">元</template>
          </el-input>
        </el-form-item>

        <el-form-item label="每人限领取" prop="perLimit">
          <el-input v-model="temp.perLimit" type="number">
            <template slot="append">张</template>
          </el-input>
        </el-form-item>

        <el-form-item label="使用门槛" prop="minPoint">
          <el-input v-model="temp.minPoint">
            <template slot="prepend">满</template>
            <template slot="append">元可用</template>
          </el-input>
        </el-form-item>

        <el-form-item label="有效期" prop="startTime">
          <el-date-picker v-model="temp.startTime" type="datetime" placeholder="选择日期" />
          至
          <el-date-picker v-model="temp.endTime" type="datetime" placeholder="选择日期" />
        </el-form-item>

        <el-form-item label="备注" prop="note">
          <el-input v-model="temp.note" type="textarea" :rows="3" placeholder="请输入备注内容" />
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

import { getCoupons, createCoupon, updateCoupon, deleteCoupon } from '@/api/marketing/coupon'

import waves from '@/directive/waves' // waves directive
import { parseTime } from '@/utils'
import UploadExcelComponent from '@/components/UploadExcel/index.vue'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

const couponTypeOptions = [
  { key: 0, display_name: '全场券' },
  { key: 1, display_name: '会员赠券' },
  { key: 2, display_name: '购物券' },
  { key: 3, display_name: '注册赠券' }
]

// 根据key js 从数组找查找对应的值
const couponTypeKeyValue = couponTypeOptions.reduce((acc, cur) => {
  console.log('acc[cur.key]', acc[cur.key], 'cur.display_name', cur.display_name)
  acc[cur.key] = cur.display_name
  return acc
}, {})

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
  name: 'CategoryList',
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
    },
    couponTypeFilter(type) {
      return couponTypeKeyValue[type]
    }
  },
  data() {
    return {
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        importance: undefined,
        // status: undefined,
        title: undefined,
        type: undefined

      },
      importanceOptions: [1, 2, 3],
      calendarTypeOptions,
      sortOptions: [{ label: '升序', key: '+id' }, { label: '降序', key: '-id' }],
      statusOptions: ['published', 'draft', 'deleted'],
      couponTypeOptions,
      showReviewer: false,
      show: true,
      temp: {
        id: undefined,
        type: 0,
        name: '',
        platform: 0,
        amount: 0,
        perLimit: 0,
        minPoint: 0,
        startTime: '',
        endTime: '',
        useType: 0,
        note: '',
        // enableTime: '',
        code: '',
        memberLevel: 0
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
        startTime: [{ required: true, message: '请选择有效期', trigger: 'change' }],
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }]
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
      getCoupons(this.listQuery).then(response => {
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
        type: 0,
        name: '',
        platform: 0,
        amount: 0,
        perLimit: 0,
        minPoint: 0,
        startTime: '',
        endTime: '',
        useType: 0,
        note: '',
        enableTime: '',
        code: '',
        memberLevel: 0
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
          createCoupon(this.temp).then(() => {
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
    handleHistory(row) {
      // TODO:
      this.$notify({
        title: '成功',
        message: '开发中...',
        type: 'success',
        duration: 2000
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          tempData.timestamp = +new Date(tempData.timestamp) // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
          updateCoupon(tempData).then(() => {
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
      })
        .then(() => {
          deleteCoupon(row.id)
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
        })
        .catch((err) => {
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

